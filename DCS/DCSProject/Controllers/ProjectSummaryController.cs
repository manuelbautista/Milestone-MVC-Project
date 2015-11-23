using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DCS.BAL.DataAccess;
using System.IO;
using System.Text;
using Ionic.Zip;
using iTextSharp.text;
using Microsoft.Office.Core;
using DCS.Common.Models;
using System.Text.RegularExpressions;


namespace DCSProject.Controllers
{

    public class ProjectSummaryController : Controller
    {
        // GET: ProjectSummary
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            try
            {
                var result = ProjectSummary.GetAllProjectsSummary();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        public JsonResult GetSummaryDetail(string id)
        {
            var result = ProjectSummary.GetProjectSummaryDetail(Convert.ToInt32(id));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfDetail(string id)
        {
            ViewBag.pdfid = id;
            return View();
        }
        public ActionResult DetailHtml(int id)
        {
            ViewBag.pdfid = id;
            return View();
        }
        public ActionResult PdfDetailDownload(int id)
        {
            ViewBag.pdfid = id;
            return View();
        }



        [HttpPost]
        public ActionResult DownloadPdf(FormCollection fc)
        {
            ActionResult returnurl = RedirectToAction("Index");
            List<string> files = new List<string>();
            string filePath = null;
            int ChkCount = !String.IsNullOrEmpty((fc["hdnchkcount"])) ? Convert.ToInt32(fc["hdnchkcount"]) : 0;
            string fileType = !string.IsNullOrEmpty(fc["downloadType"]) ? fc["downloadType"].ToString() : "";
            if (fileType == "PPT")
            {
                string[] projectIds = fc["projectIds"].ToString().TrimEnd(',').Split(',');
                string allFileName = "";
                if (projectIds.Length > 0)
                {
                    for (int i = 0; i < projectIds.Length; i++)
                    {
                        string PPT_FileName = ConvertHtmlToPpt(Convert.ToInt32(projectIds[i]));
                        allFileName += PPT_FileName + "~";
                    }
                    string[] allFileNameArr = allFileName.TrimEnd('~').Split('~');
                    if (allFileNameArr.Length > 0)
                    {
                        for (int i = 0; i < allFileNameArr.Length; i++)
                        {
                            files.Add(Server.MapPath("~/Temp/" + allFileNameArr[i].ToString() + ".ppt"));
                        }
                        DownloadFiles(files);
                    }
                }
            }
            else
            {
                if (ChkCount > 0)
                {
                    for (int i = 0; i < ChkCount; i++)
                    {
                        string Data = !String.IsNullOrEmpty((fc["multiplePDFdiv_" + i])) ? System.Uri.UnescapeDataString(fc["multiplePDFdiv_" + i]) : "";
                        filePath = GenertePDF(Data);
                        files.Add(filePath);
                    }
                    DownloadFiles(files);
                }
                else
                {
                    returnurl = RedirectToAction("Index");
                }
            }
            return returnurl;

        }
        protected void DownloadFiles(List<string> files)
        {
            if (files.Count == 1)
            {

                FileInfo fileInfo = new FileInfo(files[0]);

                Response.Clear();
                Response.BufferOutput = false;
                string fileName = String.Format("ProjectSummary_{0}" + fileInfo.Extension, DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));

                if (fileInfo.Extension == ".pdf")
                    Response.ContentType = "application/pdf";

                if (fileInfo.Extension == ".ppt")
                    Response.ContentType = "application/powerpoint";

                if (fileInfo.Extension == ".pptx")
                    Response.ContentType = "application/powerpoint";

                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);

                byte[] fileContent = System.IO.File.ReadAllBytes(files[0]);

                Response.OutputStream.Write(fileContent, 0, fileContent.Length);
                
                Response.End();
            }
            else { 
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("Project Summary Detail");
                    foreach (string filePath in files)
                    {
                        zip.AddFile(filePath, "Project Summary Detail");
                    }
                    Response.Clear();
                    Response.BufferOutput = false;
                    string fileName = String.Format("ProjectSummaryZip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    zip.Save(Response.OutputStream);
                    Response.End();
                }
            }
        }

        [ValidateInput(false)]
        public string GenertePDF(string pdfData)
        {
            StringWriter sw = new StringWriter();
            string css = System.IO.File.ReadAllText(Server.MapPath("~/Content/bootstrap.min.css"));
            string responsive = System.IO.File.ReadAllText(Server.MapPath("~/Content/responsive.css"));
            string html = "<html><head><style>" 
                + css 
                + "#header{display:none;}#printbtn{display:none;}@media screen and (max-width:768px){body{font-size:11px!important;}#mainDiv{font-size:11px!important;}.s-p-sign>.p-sign{min-width: 22%;padding: 8px 4px; margin-right:2px; font-size: 10px;text-align: center;word-wrap: break-word;width: auto;}.legendmilestone2 {font-weight: normal; font-size: 11px;padding: 8px 4px; margin-right:2px; margin-top:10px;display:inline-block;}.fa-stop{transform: rotate(45deg);}}</style></head><body >" 
                + pdfData 
                + "</body></html>";

            string path = Server.MapPath("~/Temp/PDF" + Guid.NewGuid().ToString().Substring(0, 6) + ".html");


            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(html);
                fs.Write(info, 0, info.Length);

            }
            string pdfFileName = "ProjectSummaryDetail" + Guid.NewGuid().ToString().Substring(0, 6) + ".pdf";
            string filePath = Server.MapPath("~/Temp/" + pdfFileName);
            StringBuilder sb = new StringBuilder();
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            htmlToPdf.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
            var coverHTML = "";
            htmlToPdf.GeneratePdfFromFile(path, coverHTML, filePath);
            System.IO.File.Delete(path);
            return filePath;
        }

        //==================================
        public string ConvertHtmlToPpt(int projectId)
        {
            string pptFileName = string.Empty;
            try
            {
                ProjectSummaryDetailModel objSummaryModel = new ProjectSummaryDetailModel();
                objSummaryModel = ProjectSummary.GetProjectSummaryDetail(projectId);

                string pptFilePath = string.Empty;
                string htmlFilePath = string.Empty;

                StringWriter sw = new StringWriter();

                // Create new PPTX file for the attachment purposes.

                pptFileName = projectId + "ProjectName" + Guid.NewGuid().ToString().Substring(0, 6);
                pptFilePath = Server.MapPath("~/Temp/" + pptFileName);

                Microsoft.Office.Interop.PowerPoint.Application powerPointApp = new Microsoft.Office.Interop.PowerPoint.Application();

                // VPS 28-10-2015

                powerPointApp.Visible = MsoTriState.msoTrue;
                Microsoft.Office.Interop.PowerPoint.Application PowerPoint_App = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentations multi_presentations = PowerPoint_App.Presentations;
                Microsoft.Office.Interop.PowerPoint.Presentation presentation = multi_presentations.Open(Server.MapPath("~/Temp/Template/Slide_Template.pptx"), MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);

                // Copy The Master presentation to the New presentation

                // Now open this new presentation slide and alter this with dynamic data

                for (int i = 0; i < presentation.Slides.Count; i++)
                {
                    foreach (var item in presentation.Slides[i + 1].Shapes)
                    {
                        var shape = (Microsoft.Office.Interop.PowerPoint.Shape)item;
                        if (shape.HasTable == MsoTriState.msoTrue)
                        {
                            int columnCount = shape.Table.Columns.Count;
                            if (columnCount == 13) // Detect if this is the duration Table
                            {
                                if (objSummaryModel.ProjectDurationList != null && objSummaryModel.ProjectDurationList.Count > 0)
                                {
                                    // Find single column Width
                                    var column1 = shape.Table.Columns[1];
                                    float column1Width = column1.Width;
                                    float tableHeight = shape.Height;
                                    // Calculate Table Width [Multiply with 7 as initial 7 columns have proper design]
                                    float tableWidth = column1Width * 13;

                                    // Get Total Records
                                    int recordsCount = objSummaryModel.ProjectDurationList.Count;
                                    if (recordsCount < 13)
                                    {
                                        for (int lastColIndex = columnCount; lastColIndex >= recordsCount + 1; lastColIndex--)
                                        {
                                            var colToDelete = shape.Table.Columns[lastColIndex];
                                            colToDelete.Delete();
                                        }
                                    }
                                    // Get new Width of Each Columns;
                                    float newColsWidth = tableWidth / recordsCount;

                                    for (int rec = 0; rec <= recordsCount - 1; rec++)
                                    {
                                        //shape.Table.Columns.Add(-1);
                                        var column = shape.Table.Columns[rec + 1];//[1];
                                        column.Width = newColsWidth;
                                        var cell = shape.Table.Cell(1, rec + 1);//shape.Table.Columns.Count);
                                        var projectDuration = objSummaryModel.ProjectDurationList.ElementAt(rec);
                                        cell.Shape.TextFrame.TextRange.Text = projectDuration.Title;
                                    }
                                    // Delete the default column
                                    //column1.Delete();
                                    shape.Width = tableWidth;
                                    shape.Height = tableHeight;
                                }
                            }
                        }

                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            if (shape.TextFrame.HasText == MsoTriState.msoTrue)
                            {
                                var textRange = shape.TextFrame.TextRange;
                                var text = textRange.Text;

                                if (text.IndexOf("Color_O") != -1)
                                {
                                    shape.TextFrame.TextRange.Replace("Color_O", "");//objSummaryModel.OverallStatusId.ToString());
                                    FillShapeColor(shape, objSummaryModel.OverallStatusId);
                                }

                                if (text.IndexOf("Color_Sp") != -1)
                                {
                                    shape.TextFrame.TextRange.Replace("Color_Sp", "");//objSummaryModel.ScopeId.ToString());
                                    FillShapeColor(shape, objSummaryModel.ScopeId);
                                }

                                if (text.IndexOf("Color_Sc") != -1)
                                {
                                    shape.TextFrame.TextRange.Replace("Color_Sc", "");//objSummaryModel.ScheduleId.ToString());
                                    FillShapeColor(shape, objSummaryModel.ScheduleId);
                                }

                                if (text.IndexOf("Color_R") != -1)
                                {
                                    shape.TextFrame.TextRange.Replace("Color_R", "");//objSummaryModel.ResourceId.ToString());
                                    FillShapeColor(shape, objSummaryModel.ResourceId);
                                }

                                if (text.IndexOf("Color_F") != -1)
                                {
                                    shape.TextFrame.TextRange.Replace("Color_F", "");//objSummaryModel.FinancialId.ToString());
                                    FillShapeColor(shape, objSummaryModel.FinancialId);
                                }

                                if (text.IndexOf("Slide_date") != -1)
                                    shape.TextFrame.TextRange.Replace("Slide_date", DateTime.Now.ToString("MM/dd/yyyy"));

                                if (text.IndexOf("Project_Title") != -1)
                                    shape.TextFrame.TextRange.Replace("Project_Title", objSummaryModel.ProjectName);

                                if (text.IndexOf("Project_Owner") != -1)
                                    shape.TextFrame.TextRange.Replace("Project_Owner", objSummaryModel.AnthemOwner);

                                if (text.IndexOf("Project_CoOwner") != -1)
                                    shape.TextFrame.TextRange.Replace("Project_CoOwner", objSummaryModel.PartnerName);

                                if (text.IndexOf("start_Date") != -1)
                                    shape.TextFrame.TextRange.Replace("start_Date", objSummaryModel.ProjectStartDateString);

                                if (text.IndexOf("end_Date") != -1)
                                    shape.TextFrame.TextRange.Replace("end_Date", objSummaryModel.ProjectEndDateString);

                                if (text.IndexOf("perc_Completed") != -1)
                                    shape.TextFrame.TextRange.Replace("perc_Completed", Convert.ToString(objSummaryModel.ProjectComplete));

                                if (text.IndexOf("project_desc_And_Buss_Value") != -1)
                                    shape.TextFrame.TextRange.Replace("project_desc_And_Buss_Value", (!String.IsNullOrEmpty(objSummaryModel.ProjectDescription) ? ReplaceHtmlTags(objSummaryModel.ProjectDescription) : ""));

                                if (text.IndexOf("Project_ExecutionSummary") != -1)
                                    shape.TextFrame.TextRange.Replace("Project_ExecutionSummary", (!String.IsNullOrEmpty(objSummaryModel.ProjectExecutiveSummary) ? ReplaceHtmlTags(objSummaryModel.ProjectExecutiveSummary) : ""));//"The Server Standard Platforms Program provides standard server specifications and pricing in support of:\n Technology Strategies and Planning roadmaps\nInfrastructure Lifecycle Management objectives\nIT Finance Cost Recovery goals\nDemand Management targets\nData Center Services platform Build best practices\nData Center Services platform Run fault isolation/problem determination processes\n\nEssential in providing this support is the Standard Platforms Offerings Quick Reference Guide (SPO QRG). This set of documentation maps  server, operating system, middleware, and storage speculations to costs.");

                                if (text.IndexOf("Project_accomplishments") != -1)
                                    shape.TextFrame.TextRange.Replace("Project_accomplishments", (!String.IsNullOrEmpty(objSummaryModel.ProjectAccomplishments) ? ReplaceHtmlTags(objSummaryModel.ProjectAccomplishments) : ""));//"SPO QRG V09.02.20 published on schedule\n Updated IBM Contract Rates\nEnhanced SPO Cost Codes with OS & Middleware version\nRe-designed Engineering Extract for “One Way Ordering\"");

                                if (text.IndexOf("Project_Issues") != -1)
                                    shape.TextFrame.TextRange.Replace("Project_Issues", (!String.IsNullOrEmpty(objSummaryModel.ProjectKeyRiskIssues) ? ReplaceHtmlTags(objSummaryModel.ProjectKeyRiskIssues) : ""));// "Current documentation tool (Excel) may not be adequate to manage future requirements:\nIBM/ANTM 2015 contract added Harrisonburg and Virginia Beach to data center  options\nIBM/ANTM 2015 contract added IBM hosted cloud based services options\nService Now catalog integration requirements may be difficult to implement and maintain\nServer automated configuration tools integration requirements may be difficult to implement and maintain");

                                if (text.IndexOf("Funding_Ask") != -1)
                                    shape.TextFrame.TextRange.Replace("Funding_Ask", (!String.IsNullOrEmpty(objSummaryModel.ProjectFundingAsk) ? ReplaceHtmlTags(objSummaryModel.ProjectFundingAsk) : ""));// "Capital");

                                if (text.IndexOf("direction_Needed") != -1)
                                    shape.TextFrame.TextRange.Replace("direction_Needed", (!String.IsNullOrEmpty(objSummaryModel.ProjectDirection) ? ReplaceHtmlTags(objSummaryModel.ProjectDirection) : ""));// "Approval to develop a database SPO Catalog tool to replace the SPO Quick Reference Guide Excel workbook.");
                            }
                        }
                    }
                }

                for (int i = 0; i < presentation.Slides.Count; i++)
                {
                    foreach (var item in presentation.Slides[i + 1].Shapes)
                    {
                        var shape = (Microsoft.Office.Interop.PowerPoint.Shape)item;

                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            if (shape.TextFrame.HasText == MsoTriState.msoTrue)
                            {
                                var textRange = shape.TextFrame.TextRange;
                                var text = textRange.Text;

                                if (objSummaryModel.ProjectDurationList != null && objSummaryModel.ProjectDurationList.Count > 0)
                                {
                                    int recordsCount = objSummaryModel.ProjectDurationList.Count;
                                    for (int rec = 0; rec <= recordsCount - 1; rec++)
                                    {
                                        var projectDuration = objSummaryModel.ProjectDurationList.ElementAt(rec);
                                        if (projectDuration.MileStoneList != null && projectDuration.MileStoneList.Count > 0)
                                        {
                                            int j = 0;
                                            foreach (var milestone in projectDuration.MileStoneList)
                                            {
                                                if (text == "_" + Convert.ToString(rec) + Convert.ToString(j) + "dsc")
                                                {
                                                    Microsoft.Office.Interop.PowerPoint.Slide slide = presentation.Slides[1];
                                                    switch (milestone.MilestoneStatus)
                                                    {
                                                        case 1:
                                                            {
                                                                Microsoft.Office.Interop.PowerPoint.Shape pic = slide.Shapes.AddPicture(Server.MapPath("~/Temp/Template/1.jpg"), Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, shape.Left, shape.Top, 10, 10);
                                                                break;
                                                            }
                                                        case 2:
                                                            {

                                                                Microsoft.Office.Interop.PowerPoint.Shape pic = slide.Shapes.AddPicture(Server.MapPath("~/Temp/Template/2.jpg"), Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, shape.Left, shape.Top, 10, 10);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Microsoft.Office.Interop.PowerPoint.Shape pic = slide.Shapes.AddPicture(Server.MapPath("~/Temp/Template/3.jpg"), Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, shape.Left, shape.Top, 10, 10);
                                                                break;
                                                            }
                                                    }
                                                    textRange.Text = "  " + milestone.Title;
                                                }
                                                j++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Remove all unused shapes 
                for (int i = 0; i < presentation.Slides.Count; i++)
                {
                    foreach (var item in presentation.Slides[i + 1].Shapes)
                    {
                        var shape = (Microsoft.Office.Interop.PowerPoint.Shape)item;

                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            if (shape.TextFrame.HasText == MsoTriState.msoTrue)
                            {
                                var textRange = shape.TextFrame.TextRange;
                                var text = textRange.Text;

                                if (text.IndexOf("dsc") != -1)
                                {
                                    shape.TextFrame.DeleteText();
                                }
                            }
                        }
                    }
                }

                presentation.SaveAs(pptFilePath, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPresentation, MsoTriState.msoTrue);
                presentation.Close();
                PowerPoint_App.Quit();
                // VPS Code Ends Here

            }

            catch (Exception ex)
            {
                pptFileName = string.Empty;
            }
            return pptFileName;
        }

        public void FillShapeColor(Microsoft.Office.Interop.PowerPoint.Shape shape, int colorId)
        {
            switch (colorId)
            {
                case 1:
                    {
                        shape.Fill.ForeColor.RGB = ToBgr(255, 0, 0);
                        break;
                    }
                case 2:
                    {
                        shape.Fill.ForeColor.RGB = ToBgr(255, 255, 0);
                        break;
                    }
                case 3:
                    {
                        shape.Fill.ForeColor.RGB = ToBgr(0, 128, 0);
                        break;
                    }
            }
        }
        public int ToBgr(int r, int g, int b)
        {
            //  & 0xFFFFFF -> Strip away alpha channel which powerpoint doesn't like
            return System.Drawing.Color.FromArgb(b, g, r).ToArgb() & 0xFFFFFF;
        }

        public string ReplaceHtmlTags(string html = "")
        {
            return Regex.Replace(html, @"<[^>]+>|&nbsp;", "").Trim();
        }
    }
}