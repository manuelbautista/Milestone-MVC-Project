using DCS.BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DCS.DAL;
using DCS.Common.Models;
using DCS.BAL.DataAccess;
using AutoMapper;
using System.Linq.Expressions;
using DCS.Common.DTO;
using DCS.Common.Helpers;
using System.Globalization;
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using ClosedXML.Excel;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using System.Web.UI;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Core;
using Ionic.Zip;

namespace DCSProject.Controllers
{
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class ProjectController : Controller
    {

        #region Properties

        #endregion


        //
        // GET: /Project/
        //ETOPlaybookService obj;
        private IRepository<Project, ProjectModel> _Project;
        private IRepository<ETOPlaybook, ETOPlaybookModel> _ETOPlaybook;
        private IRepository<ProjectOwner, ProjectOwnerModel> _ProjectOwner;
        private IRepository<ProjectStatu, ProjectStatusModel> _ProjectStatus;
        private IRepository<ProjectMilestone, ProjectMilestoneModel> _ProjectMilestone;
        private IRepository<ProjectSchedule, ProjectScheduleModel> _ProjectSchedule;
        private IRepository<ProjectMultiEmail, ProjectMultiEmailModel> _projectEmail;
        private IRepository<TimestampDetail, TimestampDetailModel> _timestampDetail;

        public ProjectController()
        {
            _Project = new Repository<Project, ProjectModel>();
            _ETOPlaybook = new Repository<ETOPlaybook, ETOPlaybookModel>();
            _ProjectOwner = new Repository<ProjectOwner, ProjectOwnerModel>();
            _ProjectStatus = new Repository<ProjectStatu, ProjectStatusModel>();
            _ProjectMilestone = new Repository<ProjectMilestone, ProjectMilestoneModel>();
            _ProjectSchedule = new Repository<ProjectSchedule, ProjectScheduleModel>();
            _projectEmail = new Repository<ProjectMultiEmail, ProjectMultiEmailModel>();
            _timestampDetail = new Repository<TimestampDetail, TimestampDetailModel>();
        }

        public ActionResult Index()
        {
            try
            {
                IEnumerable<GetAllProjectsIndex_Result> projects = _Project.GetProjects();
                return View(projects);
            }
            catch (Exception ex)
            {
                ProjectController.TraceServiceStatus(Server.MapPath("~/ProjectLog.txt"), ex.Message + "  " + ex.StackTrace);
                return View(_Project.GetProjects());
            }
        }
        //  [Authorize(Users = @"us/STDP18,us/CARRODO")]
        //    [Authorize(Users = @"nextolive\naina.verma")]
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var project = new ProjectModel();
            IEnumerable<ETOPlaybookModel> etoPlayBook = _ETOPlaybook.GetAll();
            IEnumerable<ProjectOwnerModel> projectOwner = _ProjectOwner.GetAll();
            if (id > 0)
            {
                Expression<Func<Project, bool>> filter = x => x.ID == id;
                ProjectModel objProject = _Project.Get(filter);
                Expression<Func<ProjectSchedule, bool>> scheduleFilter = x => x.ProjectId == id;
                ProjectScheduleModel objProjectSchedule = _ProjectSchedule.Get(scheduleFilter);
                objProject.ProjectStartDate = objProjectSchedule.StartDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                objProject.ProjectEndDate = objProjectSchedule.EndDate.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                ViewBag.ProjectOwnerNameANTM = projectOwner.Where(x => x.Type == 1).ToList();
                ViewBag.ProjectOwnerNameExternal = projectOwner.Where(x => x.Type == 2).ToList();
                ViewBag.ETOPlaybook = etoPlayBook;
                ViewBag.ProjectID = objProject.ID;
                return View(objProject);
            }
            project.ProjectEndDate = "";
            project.ProjectStartDate = "";
            ViewBag.ProjectID = project.ID;
            ViewBag.EOPlaybook = GetEOPlaybook();
            ViewBag.ManagementApproval = GetManagementApproval();
            ViewBag.PeerReview = GetPeerReview();
            ViewBag.ETOPlaybook = etoPlayBook;
            ViewBag.ProjectOwnerNameANTM = projectOwner.Where(x => x.Type == 1).ToList();
            ViewBag.ProjectOwnerNameExternal = projectOwner.Where(x => x.Type == 2).ToList();
            return View(project);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProjectModel model, string StartDate, string EndDate)
        {
            bool isProjectNumberExist = false;

            if (model.ID == 0)
            {
                if (ModelState.IsValid)
                {
                    isProjectNumberExist = _Project.IsProjectNumberExist(model.ProjectNumber);
                    if (!isProjectNumberExist)
                    {
                        string[] TableColumn = { "ScheduleId", "ResourcesId", "ScopeId", "FinancialId", "ProjectStatusId", "StartDate", "EndDate", "Complete", "Name", "ProjectOwnerANTMId", "ProjectOwnerExternalId", "ProjectNumber", "EOPlaybook", "PeerReview" };
                        //Project custExisting;
                        Mapper.CreateMap<ETOPlaybookModel, ETOPlaybook>();
                        Mapper.CreateMap<ProjectModel, Project>();
                        Mapper.CreateMap<ETOPlaybookDTO, ETOPlaybook>();
                        Mapper.CreateMap<ProjectOwnerModel, ProjectOwner>();
                        var result = Mapper.Map<ProjectModel, Project>(model);
                        result.Created = DateTime.Now;
                        result.Modified = DateTime.Now;
                        result.IsActive = true;
                        _Project.Add(result);
                        int ProjectId = _Project.GetTopProjectId();
                        ProjectStatu objProjectStatus = new ProjectStatu();
                        objProjectStatus.ScheduleId = 3;
                        objProjectStatus.ResourcesId = 3;
                        objProjectStatus.ScopeId = 3;
                        objProjectStatus.FinancialId = 3;
                        objProjectStatus.ProjectStatusId = 3;
                        objProjectStatus.ProjectId = ProjectId;
                        objProjectStatus.ModifiedDate = DateTime.Now;
                        _ProjectStatus.Add(objProjectStatus);
                        ProjectSchedule obj = new ProjectSchedule();
                        obj.ProjectId = ProjectId;
                        obj.StartDate = DateTime.ParseExact(StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(StartDate);
                        obj.EndDate = DateTime.ParseExact(EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);////Convert.ToDateTime(EndDate);
                        obj.Complete = 0;
                        _ProjectSchedule.Add(obj);
                        for (int i = 0; i < TableColumn.Length; i++)
                        {
                            TimestampDetail objTimestamp = new TimestampDetail();
                            objTimestamp.ProjectId = ProjectId;
                            objTimestamp.ColumnName = TableColumn[i].ToString();
                            objTimestamp.ModifiedDate = DateTime.Now;
                            _timestampDetail.Add(objTimestamp);
                        }


                        return RedirectToAction("index", "project");
                    }
                    else
                    {
                        ViewData["message"] = "The project number entered already exists.  Enter a unique project number.";
                        IEnumerable<ETOPlaybookModel> etoPlayBook = _ETOPlaybook.GetAll();
                        IEnumerable<ProjectOwnerModel> projectOwner = _ProjectOwner.GetAll();
                        ViewBag.EOPlaybook = GetEOPlaybook();
                        ViewBag.ManagementApproval = GetManagementApproval();
                        ViewBag.PeerReview = GetPeerReview();
                        ViewBag.ETOPlaybook = etoPlayBook;
                        ViewBag.ProjectOwnerNameANTM = projectOwner.Where(x => x.Type == 1).ToList();
                        ViewBag.ProjectOwnerNameExternal = projectOwner.Where(x => x.Type == 2).ToList();
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    bool result = _Project.UpdateProject(model);
                    Decimal PercentageComplete = _Project.GetProjectPercentageCompleteByProjectId(model.ID).GetValueOrDefault();
                    bool projectScheduleUpdateResult = _ProjectSchedule.UpdateProjectSchedule(StartDate, EndDate, model.ID, PercentageComplete);
                }
                return RedirectToAction("index", "project");
            }

        }

        // [Authorize(Users = @"us/STDP18,us/CARRODO")]
        public ActionResult AddProjectOwnerNameANTM()
        {
            return View();
        }
        // [Authorize(Users = "us/STDP18,us/CARRODO")]
        public ActionResult AddProjectOwnerNameExternal()
        {
            return View();
        }
        // [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpGet]
        public ActionResult EditMilestone(int id = 0)
        {

            var Project = new ProjectModel();
            IEnumerable<ETOPlaybookModel> etoPlayBook = _ETOPlaybook.GetAll();
            IEnumerable<ProjectOwnerModel> projectOwner = _ProjectOwner.GetAll();
            if (id > 0)
            {
                Expression<Func<ProjectStatu, bool>> filter = x => x.ProjectId == id;
                ProjectStatusModel projectStatus = _ProjectStatus.Get(filter);
                ViewBag.ProjectStatus = projectStatus;
                GetDetailForProject_Result projectDetail = _Project.GetProjectById(id);
                ViewBag.RequirementList = GetRequirementList();
                return View(projectDetail);

            }
            ViewBag.RequirementList = GetRequirementList();
            return View();
        }
        //  [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditMilestone(GetDetailForProject_Result model, FormCollection fc)
        {
            string pdfData = !String.IsNullOrEmpty((fc["PDFdiv"])) ? System.Uri.UnescapeDataString(fc["PDFdiv"]) : "";
            string detailHtmlData = !String.IsNullOrEmpty((fc["DetailHtmlDiv"])) ? System.Uri.UnescapeDataString(fc["DetailHtmlDiv"]) : "";
            string complete = !String.IsNullOrEmpty(fc["hdnPercentage"]) ? Convert.ToString(fc["hdnPercentage"]) : "";
            string MsgBody = !String.IsNullOrEmpty(fc["hdnMsgBody"]) ? Convert.ToString(fc["hdnMsgBody"]) : "";
            string ImagePath = !String.IsNullOrEmpty(fc["hdnImagePath"]) ? Convert.ToString(fc["hdnImagePath"]) : "";
            int ScopeId = model.ScopeId != null ? model.ScopeId.GetValueOrDefault() : 0;
            int ScheduleId = model.ScheduleId != null ? model.ScheduleId.GetValueOrDefault() : 0;
            int ResourcesId = model.ResourcesId != null ? model.ResourcesId.GetValueOrDefault() : 0;
            int FinancialId = model.FinancialId != null ? model.FinancialId.GetValueOrDefault() : 0;
            int ID = model.ID != null ? model.ID : 0;
            bool result = SaveMilestone(complete, MsgBody, ImagePath, ScopeId, ScheduleId, ResourcesId, FinancialId, ID, pdfData, detailHtmlData);


            //if(result)
            //{ }

            return RedirectToAction("Index");
        }
        //  [Authorize(Users = "us/STDP18,us/CARRODO")]
        public bool SaveMilestone(string complete, string MsgBody, string ImagePath, int ScopeId, int ScheduleId, int ResourcesId, int FinancialId, int ID, string pdfData, string detailHtmlData)
        {

            string PdfFileName = null;
            string pptFileName = string.Empty;
            ProjectStatusModel ps = new ProjectStatusModel();
            if (ScopeId > 0 && ScopeId <= 4)
            {
                ps.ScopeId = ScopeId;
            }
            else
            {
                ps.ScopeId = 0;
            }
            if (ScheduleId > 0 && ScheduleId <= 4)
            {
                ps.ScheduleId = ScheduleId;
            }
            else
            {
                ps.ScheduleId = 0;
            }
            if (ResourcesId > 0 && ResourcesId <= 4)
            {
                ps.ResourcesId = ResourcesId;
            }
            else
            {
                ps.ResourcesId = 0;
            }
            if (FinancialId > 0 && FinancialId <= 4)
            {
                ps.FinancialId = FinancialId;
            }
            else
            {
                ps.FinancialId = 0;
            }
            if (ScopeId == 1 || ScheduleId == 1 || ResourcesId == 1 || FinancialId == 1)
            {
                ps.ProjectStatusId = 1;
            }
            else if (ScopeId == 2 || ScheduleId == 2 || ResourcesId == 2 || FinancialId == 2)
            {
                ps.ProjectStatusId = 2;
            }
            else if (ScopeId == 3 && ScheduleId == 3 && ResourcesId == 3 && FinancialId == 3)
            {
                ps.ProjectStatusId = 3;
            }
            else
            {
                ps.ProjectStatusId = 0;
            }
            ps.ProjectId = ID;
            _ProjectStatus.UpdateProjectStatus(ps);
            DCS.Common.Models.ProjectScheduleModel scheduleModel = _ProjectSchedule.Get(x => x.ProjectId == ID);
            scheduleModel.Complete = !String.IsNullOrEmpty(complete) ? Convert.ToDecimal(complete) : 0;
            _ProjectSchedule.UpdateProjectSchedule(scheduleModel.StartDate.ToShortDateString(), scheduleModel.EndDate.Value.ToShortDateString(), scheduleModel.ProjectId, scheduleModel.Complete);

            //string html = System.Web.Mvc.Html.Action("PdfDetail", "ProjectSummary", new { id = ID });
            // string html =  RenderRazorViewToString("PdfDetail");

            if (MsgBody != null && MsgBody != "")
            {

                PdfFileName = GenertePDF(pdfData);
                //pptFileName = ConvertHtmlToPpt(ID);
                var result = _projectEmail.GetProjectMultiEmail(ID);
                if (result != null && result.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Start Date", typeof(string));
                    dt.Columns.Add("% Complete", typeof(decimal));
                    dt.Columns.Add("Status", typeof(string));


                    var mresult = ProjectMileStone.GetProjectMileStones(ID);
                    if (mresult != null && mresult.Count > 0)
                    {
                        ProjectMilestoneModel objmilestone = new ProjectMilestoneModel();
                        foreach (var itm in mresult)
                        {

                            string name = itm.Name != null ? Convert.ToString(itm.Name) : "";
                            string date = itm.StartDate != null ? itm.StartDate.ToShortDateString() : "";
                            decimal percentage = itm.Complete != null ? itm.Complete : 0;
                            int statuscode = itm.MilestoneStatus;
                            string statusName = GetStatusName(statuscode);

                            dt.Rows.Add(name, date, percentage, statusName);
                        }
                    }


                    string mailSubject = "DCSPP Status Notification";
                    System.Threading.Tasks.Parallel.ForEach(result, row =>
                    {
                        SendEmail(row.Email, mailSubject, MsgBody, true, dt, ImagePath, PdfFileName, pptFileName);
                    });
                }
            }

            return true;
        }


        [ValidateInput(false)]
        public string GenertePDF(string pdfData)
        {
            StringWriter sw = new StringWriter();
            //string html = System.Net.WebUtility.HtmlDecode(pdfData);
            string css = System.IO.File.ReadAllText(Server.MapPath("~/Content/bootstrap.min.css"));
            //string newstyle = System.IO.File.ReadAllText(Server.MapPath("/Content/newstyle.css"));
            string responsive = System.IO.File.ReadAllText(Server.MapPath("~/Content/responsive.css"));

            string html = "<html><head><style>" + css + "#header{display:none;}#printbtn{display:none;}@media screen and (max-width:768px){body{font-size:11px!important;}#mainDiv{font-size:11px!important;}.s-p-sign>.p-sign{min-width: 22%;padding: 8px 4px; margin-right:2px; font-size: 10px;text-align: center;word-wrap: break-word;width: auto;}.legendmilestone2 {font-weight: normal; font-size: 11px;padding: 8px 4px; margin-right:2px; margin-top:10px;display:inline-block;}.fa-stop{transform: rotate(45deg);}}</style></head><body >" + pdfData + "</body></html>";

            string path = Server.MapPath("~/Temp/PDF" + Guid.NewGuid().ToString().Substring(0, 6) + ".html");

            // Create the file.
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(html);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);

            }
            string pdfFileName = "ProjectSummaryDetail" + Guid.NewGuid().ToString().Substring(0, 6) + ".pdf";
            string filePath = Server.MapPath("~/Temp/" + pdfFileName);
            StringBuilder sb = new StringBuilder();
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var coverHTML = "";
            htmlToPdf.GeneratePdfFromFile(path, coverHTML, filePath);
            System.IO.File.Delete(path);
            return pdfFileName;
        }

        public string GetStatusName(int value)
        {
            var result = "";
            if (value == 1)
            {
                result = "Planned";  //Not Started
            }
            if (value == 2)
            {
                result = "In Progress"; //In Progress
            }
            if (value == 3)
            {
                result = "Completed"; //Completed
            }
            if (value == 0)
            {
                result = "Not Started";
            }
            return result;
        }

        #region Method : SendEmail  parameter
        public static bool SendEmail(string toMail, string subject, string message, bool isBodyHtml, DataTable dt, string ImagePath, string PdfFileName, string pptFileName)
        {
            try
            {
                string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
                dt.TableName = "Project_Data";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    IXLWorksheet sheet = wb.Worksheets.Add(dt);
                    sheet.FirstRow().Style.Fill.SetBackgroundColor(XLColor.FromArgb(122, 207, 251));
                    MailMessage msg = new MailMessage(mailFrom, toMail, subject, message);
                    msg.IsBodyHtml = isBodyHtml;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        wb.SaveAs(memoryStream);
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "Project_Milestone.xlsx"));
                    }
                    if (!String.IsNullOrEmpty(PdfFileName))
                    {
                        msg.Attachments.Add(new Attachment(System.Web.Hosting.HostingEnvironment.MapPath("~/Temp/" + PdfFileName)));
                    }
                    if (!String.IsNullOrEmpty(pptFileName))
                    {
                        msg.Attachments.Add(new Attachment(System.Web.Hosting.HostingEnvironment.MapPath("~/Temp/" + pptFileName + ".ppt")));
                    }
                    SetUserCredentialAndProcessMail(msg, toMail);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
        private static MemoryStream PDFGenerate(string message, string ImagePath)
        {

            MemoryStream output = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, output);

            pdfDoc.Open();
            Paragraph Text = new Paragraph(message);
            pdfDoc.Add(Text);

            byte[] file;
            file = System.IO.File.ReadAllBytes(ImagePath);

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(file);
            jpg.ScaleToFit(550, 800);
            pdfDoc.Add(jpg);

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            output.Position = 0;

            return output;
        }

        #region Method SetUserCredentialAndProcessMail
        private static void SetUserCredentialAndProcessMail(MailMessage msg, string mailTo)
        {
            string userName = ConfigurationManager.AppSettings["userName"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();

            string bccAddress = ConfigurationManager.AppSettings["bccAddress"].ToString();

            string smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
            string smtpPort = ConfigurationManager.AppSettings["Port"].ToString();

            string testMode = ConfigurationManager.AppSettings["testMode"].ToString();
            NetworkCredential cred = new NetworkCredential(userName, password);
            SmtpClient mailClient = testMode == "1" ? new SmtpClient(smtpServer, 587) : new SmtpClient(smtpServer, 25);
            mailClient.EnableSsl = testMode == "1" ? true : false;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = cred;
            try
            {
                if (testMode == "1")
                {
                    using (MailMessage mailMessage = new MailMessage(new MailAddress(mailFrom, "DCSPP"), new MailAddress(mailTo)))
                    {
                        mailMessage.Subject = msg.Subject;
                        mailMessage.Body = msg.Body;
                        mailMessage.IsBodyHtml = msg.IsBodyHtml;

                        if (msg.Bcc != null && msg.Bcc.Count > 0)
                        {
                            foreach (var item in msg.Bcc)
                            {
                                mailMessage.Bcc.Add(item);
                            }
                        }

                        if (msg.Attachments != null && msg.Attachments.Count > 0)
                        {
                            AttachmentCollection attachment = msg.Attachments;
                            foreach (Attachment item in attachment)
                            {
                                mailMessage.Attachments.Add(item);
                            }
                        }
                        mailClient.Send(mailMessage);
                    }

                }
                else
                {
                    using (MailMessage mailMessage = new MailMessage(new MailAddress(mailFrom, "DCSPP"), new MailAddress(mailTo)))
                    {
                        mailMessage.Subject = msg.Subject;
                        mailMessage.Body = msg.Body;
                        mailMessage.IsBodyHtml = msg.IsBodyHtml;
                        if (msg.Bcc != null && msg.Bcc.Count > 0)
                        {
                            foreach (var item in msg.Bcc)
                            {
                                mailMessage.Bcc.Add(item);
                            }
                        }
                        if (msg.Attachments != null && msg.Attachments.Count > 0)
                        {
                            AttachmentCollection attachment = msg.Attachments;
                            foreach (Attachment item in attachment)
                            {
                                mailMessage.Attachments.Add(item);
                            }
                        }
                        mailClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public static string RenderRazorViewToString<T>(string viewName, string masterName, T model, System.Web.Mvc.ControllerContext controllerContext)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        // [Authorize(Users = "us/STDP18,us/CARRODO")]
        public JsonResult AddProjectOwner(ProjectOwnerModel model)
        {
            ProjectOwner objProjectOwner = new ProjectOwner();
            string[] fullName = model.FullName.Split(' ');
            objProjectOwner.FirstName = fullName[0];
            if (fullName.Length == 1)
            {
                objProjectOwner.LastName = "";
            }
            else
            {
                objProjectOwner.LastName = fullName[1];
            }
            objProjectOwner.Email = model.Email;
            objProjectOwner.IsActive = true;
            objProjectOwner.Type = model.Type;     //For ProjectOwnerANTM
            objProjectOwner.Created = DateTime.Now;
            _ProjectOwner.Add(objProjectOwner);
            int ownerId = _ProjectOwner.GetTopOwnerId();
            return Json(ownerId, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> GetEOPlaybook(int selectedid = 0)
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="2",Text="-Select-"},
                 new SelectListItem{ Value="1",Text="Yes"},
                 new SelectListItem{ Value="0",Text="No"}
             };

            myList = data.ToList();

            return myList;
        }
        public IEnumerable<SelectListItem> GetManagementApproval()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text="-Select-"},
                 new SelectListItem{ Value="1",Text="Yes"},
                 new SelectListItem{ Value="2",Text="No"}
             };
            myList = data.ToList();
            return myList;
        }
        public IEnumerable<SelectListItem> GetPeerReview()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text="-Select-"},
                 new SelectListItem{ Value="1",Text="Yes"},
                 new SelectListItem{ Value="2",Text="No"}
             };
            myList = data.ToList();
            return myList;
        }

        public IEnumerable<SelectListItem> GetProjectOwnerNameANTM()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text="-Select-"},
                 new SelectListItem{ Value="1",Text="Anderson, Bill"},
                 new SelectListItem{ Value="2",Text="Boyer, Kevin"},
                 new SelectListItem{ Value="3",Text="DAVIS, JACQUELINE A"},
                 new SelectListItem{ Value="4",Text="MULCONRY, MARK L"},
                 new SelectListItem{ Value="5",Text="Peddicord, Shawn T."},
                 new SelectListItem{ Value="6",Text="Sennett, James"},
                 new SelectListItem{ Value="7",Text="WALSH, BRIAN W"},
                 new SelectListItem{ Value="8",Text="WARFIELD, JOHN N"}
             };
            myList = data.ToList();
            return myList;
        }

        public IEnumerable<SelectListItem> GetProjectOwnerNameExternal()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text="-Select-"},
                 new SelectListItem{ Value="1",Text="Osias, Michael"},
                 new SelectListItem{ Value="2",Text="Muller, Sue C."},
                 new SelectListItem{ Value="3",Text="Letendre, Duane."}
             };
            myList = data.ToList();
            return myList;
        }

        public IEnumerable<SelectListItem> GetRequirementList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="Red"},
                 new SelectListItem{ Value="2",Text="Amber"},
                 new SelectListItem{ Value="3",Text="Green"},
                 //new SelectListItem{ Value="4",Text="Completed"},
                 //new SelectListItem{ Value="0",Text="Not Started"},
             };
            myList = data.ToList();
            return myList;
        }

        public IEnumerable<SelectListItem> GetETOPlaybook()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text="-Select-"},
                 new SelectListItem{ Value="1",Text="Anthem Transformation  Affiliates Storage/SAN/BUR"},
                 new SelectListItem{ Value="2",Text="IBM Compute Transformation"},
                 new SelectListItem{ Value="3",Text="IBM virtual desktop transformation (new contract)"},
                 new SelectListItem{ Value="4",Text="Application Interface Insourcing"},
                 new SelectListItem{ Value="5",Text="Infrastructure speed-to-market/ICO"},
                 new SelectListItem{ Value="6",Text="Harrisonburg - Road to Readiness (Compute)"},
                 new SelectListItem{ Value="7",Text="Harrisonburg - Road to Readiness (Storage)"},
                 new SelectListItem{ Value="8",Text="Infrastructure Lifecycle Management"},
                 new SelectListItem{ Value="9",Text="Server patching, audit compliance"},
                 new SelectListItem{ Value="10",Text="Production mainframe upgrade"},
                 new SelectListItem{ Value="11",Text="Customer partnership enhancements"},
                 new SelectListItem{ Value="12",Text="Server Standard Platform Offerings"},
                 new SelectListItem{ Value="13",Text="Storage auto provisioning"},
                 new SelectListItem{ Value="14",Text="Dynamic automation"},
                 new SelectListItem{ Value="15",Text="Infrastructure platform health checks"},
                 new SelectListItem{ Value="16",Text="Vendor procurement analysis"},
                 new SelectListItem{ Value="17",Text="Analyze monitoring and reporting tools"},
                 new SelectListItem{ Value="18",Text="Server compute authentication efforts"},
                 new SelectListItem{ Value="19",Text="Data Retention Plan and Activation"},
             };
            myList = data.ToList();
            return myList;
        }

        public JsonResult GetMileStones(int id)
        {
            var result = ProjectMileStone.GetProjectMileStones(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEmail(int id)
        {
            var result = _projectEmail.GetProjectMultiEmail(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteMultiEmail(int id, int projectId)
        {
            usp_RemoveMultiEmail1_Result1 objPromultiemail = _projectEmail.RemoveProjectMultiEmail(id);
            var result = objPromultiemail.ProjectId;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public bool SaveEmail(string Email, int ID)
        {
            bool result = false;
            if (ID > 0)
            {
                ProjectMultiEmail objProjMail = new ProjectMultiEmail();
                objProjMail.Email = Email;
                objProjMail.ProjectId = ID;
                result = _projectEmail.Add(objProjMail);
            }

            return result;
        }

        //  [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        [ValidateInput(false)]
        public bool AddMileStone(string milestonename, string milestonestart, string milestonecomp, string milestonestatus, int ID, int milestoneid)
        {
            ProjectMilestoneModel objProjectMileStone = new ProjectMilestoneModel();
            objProjectMileStone.Name = milestonename;
            objProjectMileStone.StartDate = DateTime.ParseExact(milestonestart, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            objProjectMileStone.Complete = Convert.ToDecimal(milestonecomp);
            objProjectMileStone.MilestoneStatus = Convert.ToInt32(milestonestatus);
            objProjectMileStone.ProjectId = ID;
            objProjectMileStone.ID = milestoneid;
            // objProjectMileStone.ModifiedDate = DateTime.Now;
            bool result = ProjectMileStone.SaveProjectMileStone(objProjectMileStone);
            return result;
        }

        //  [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        public JsonResult RemoveMileStone(int id, int projectId)
        {
            ProjectMilestoneModel objProjectMileStone = new ProjectMilestoneModel();
            objProjectMileStone.ProjectId = projectId;
            objProjectMileStone.ID = id;
            var result = ProjectMileStone.RemoveProjectMileStone(objProjectMileStone);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //   [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        public ActionResult RemoveProject(int id)
        {
            var result = ProjectDetail.RemoveProject(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static void TraceServiceStatus(string path, string message)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(DateTime.Now.ToString() + "\t" + " " + message);
                writer.Flush();
                writer.Close();
            }
        }
        // [Authorize(Users = "us/STDP18,us/CARRODO")]
        public ActionResult EditProjectOwnerNameANTM()
        {
            return View();
        }


        public JsonResult BindAnthemOwner()
        {
            string result = string.Empty;
            IEnumerable<ProjectOwnerModel> projectOwner = _ProjectOwner.GetAll();
            var ownerList = projectOwner.Where(x => x.Type == 1).ToList();
            if (ownerList != null && ownerList.Count > 0)
            {
                result = Newtonsoft.Json.JsonConvert.SerializeObject(ownerList);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult BindIBMOwner()
        {
            string result = string.Empty;
            IEnumerable<ProjectOwnerModel> projectOwner = _ProjectOwner.GetAll();
            var ownerList = projectOwner.Where(x => x.Type == 2).ToList();
            if (ownerList != null && ownerList.Count > 0)
            {
                result = Newtonsoft.Json.JsonConvert.SerializeObject(ownerList);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //    [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        public ActionResult deleteAnthemOwner(int ID)
        {
            var result = ProjectDetail.RemoveOwner(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //   [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        public ActionResult updateAnthemOwner(string ID, string Name, string Email)
        {

            ProjectOwner objProjectOwner = new ProjectOwner();
            string[] fullName = Name.Split(' ');
            for (int i = 0; i < fullName.Length; i++)
            {
                if (i == 0)
                {
                    objProjectOwner.FirstName = fullName[i];
                }
                else
                {
                    objProjectOwner.LastName += " " + fullName[i];
                }
            }

            //if (fullName.Length == 1)
            //{
            //    objProjectOwner.LastName = "";
            //}
            //else
            //{
            //    objProjectOwner.LastName = fullName[1];
            //}
            objProjectOwner.Email = Email;
            objProjectOwner.IsActive = true;
            objProjectOwner.Type = 1;     //For ProjectOwnerANTM
            objProjectOwner.Created = DateTime.Now;
            objProjectOwner.ID = Convert.ToInt32(ID);
            bool result = _ProjectOwner.Update(objProjectOwner);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //  [Authorize(Users = "us/STDP18,us/CARRODO")]
        public ActionResult EditProjectOwnerNameExternal()
        {
            return View();
        }
        // [Authorize(Users = "us/STDP18,us/CARRODO")]
        [HttpPost]
        public ActionResult updateIBMOwner(string ID, string Name, string Email)
        {

            ProjectOwner objProjectOwner = new ProjectOwner();
            string[] fullName = Name.Split(' ');
            for (int i = 0; i < fullName.Length; i++)
            {
                if (i == 0)
                {
                    objProjectOwner.FirstName = fullName[i];
                }
                else
                {
                    objProjectOwner.LastName += " " + fullName[i];
                }
            }
            objProjectOwner.Email = Email;
            objProjectOwner.IsActive = true;
            objProjectOwner.Type = 2;     //For ProjectOwnerExternal
            objProjectOwner.Created = DateTime.Now;
            objProjectOwner.ID = Convert.ToInt32(ID);
            bool result = _ProjectOwner.Update(objProjectOwner);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetOverAllPercentageForProject(int projectId)
        {
            var result = _ProjectSchedule.GetEntity(x => x.ProjectId == projectId);
            if (result != null)
            {
                return (result.Complete.HasValue ? result.Complete.Value.ToString() : decimal.Zero.ToString());
            }
            else
                return decimal.Zero.ToString();
        }

        [HttpPost]
        public JsonResult SaveImage()
        {
            HttpPostedFileBase file = Request.Files.Get(0);
            string json = string.Empty;
            if ((file != null) && (file.ContentLength > 0))
            {
                string fileName = Guid.NewGuid() + file.FileName + ".jpg";
                // string fileName = "test.png";
                string filePath = System.IO.Path.Combine(HttpContext.Server.MapPath("~/Temp/Image/"), fileName);
                file.SaveAs(filePath);
                json = fileName;

            }
            else
            {

            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
       


        #region PPT
        //public void getForPPT(string projectIds)
        //{
        //    try
        //    {
        //        string[] allSelectedProjectIds = projectIds.TrimEnd(',').Split(',');
        //        string allFileName = "";
        //        if (allSelectedProjectIds.Length > 0)
        //        {
        //            for (int i = 0; i < allSelectedProjectIds.Length; i++)
        //            {
        //                string PPT_FileName = ConvertHtmlToPpt(Convert.ToInt32(allSelectedProjectIds[i]));
        //                allFileName += PPT_FileName + "~";
        //            }
        //            string[] allFileNameArr = allFileName.TrimEnd('~').Split('~');
        //            if (allFileNameArr.Length > 0)
        //            {
        //                List<string> files = new List<string>();
        //                for (int i = 0; i < allFileNameArr.Length; i++)
        //                {
        //                    files.Add(Server.MapPath("~/Temp/" + allFileNameArr[i].ToString() + ".ppt"));
        //                }
        //                DownloadFiles(files);
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}



        //protected void DownloadFiles(List<string> files)
        //{
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
        //        zip.AddDirectoryByName("Project Summary Detail");
        //        foreach (string filePath in files)
        //        {
        //            zip.AddFile(filePath, "Project Summary Detail");
        //        }
        //        Response.Clear();
        //        Response.BufferOutput = false;
        //        string zipName = String.Format("ProjectSummaryZip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
        //        Response.ContentType = "application/zip";
        //        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
        //        zip.Save(Response.OutputStream);
        //        Response.End();
        //    }

        //}
        #endregion
        public string ReplaceHtmlTags(string html = "")
        {
            return Regex.Replace(html, @"<[^>]+>|&nbsp;", "").Trim();
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
    }
}
