using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCS.DAL;
using DCS.Common.Models;
namespace DCS.BAL.DataAccess
{
    public class ProjectSummary
    {
        public static List<ProjectSummaryModel> GetAllProjectsSummary()
        {
            try
            {
                List<ProjectSummaryModel> lstSummaryModel = new List<ProjectSummaryModel>();
                using (DCSEntities db = new DCSEntities())
                {
                    var result = db.usp_GetAllProjectsSummary().ToList();
                    if (result != null && result.Count > 0)
                    {
                        foreach (var x in result)
                        {
                            ProjectSummaryModel objProjectSummaryModel = new ProjectSummaryModel();
                            objProjectSummaryModel.AnthemOwner = x.AnthemOwner;
                            objProjectSummaryModel.AnthemOwnerEmail = x.AnthemEmail;
                            objProjectSummaryModel.ExternalOwner = x.ExternalOwner;
                            objProjectSummaryModel.ExternalOwnerEmail = x.ExternalEmail;
                            objProjectSummaryModel.IsETOPlayBook = x.IsETOPlayBook;
                            objProjectSummaryModel.IsPeerReview = x.IsPeerReview;
                            objProjectSummaryModel.ProjectComplete = x.ProjectComplete;
                            objProjectSummaryModel.ProjectDaysRemaining =x.ProjectDaysRemaining.HasValue ? x.ProjectDaysRemaining.Value : decimal.MinValue;
                            objProjectSummaryModel.ProjectEffortRemaining = x.ProjectEffortRemaining;
                            objProjectSummaryModel.ProjectEndDate = x.ProjectEndDate;
                            objProjectSummaryModel.ProjectEndDateString = string.Format("{0:MM-dd-yyyy}", x.ProjectEndDate.ToShortDateString());
                            objProjectSummaryModel.ProjectFinancial =  x.ProjectFinancial;
                            objProjectSummaryModel.ProjectNumber = x.ProjectNumber;
                            objProjectSummaryModel.ProjectId = x.ProjectId;
                            objProjectSummaryModel.ProjectName = x.ProjectName;
                            objProjectSummaryModel.ProjectOverAll =  x.ProjectOverAll;
                            objProjectSummaryModel.ProjectRAGStatus = x.ProjectRAGStatus;
                            objProjectSummaryModel.ProjectResource =  x.ProjectResource;
                            objProjectSummaryModel.ProjectSchedule =  x.ProjectSchedule;
                            objProjectSummaryModel.ProjectScope =  x.ProjectScope;
                            objProjectSummaryModel.ProjectStartDate = x.ProjectStartDate;
                            objProjectSummaryModel.ProjectStartDateString = string.Format("{0:MM-dd-yyyy}", x.ProjectStartDate.ToShortDateString());
                            int ProjectModifiedDiff = x.ProjectModifedDiff.GetValueOrDefault();
                            int MilestoneModifiedDiff = x.MilestoneModifiedDiff.GetValueOrDefault();
                            int StatusModifiedDiff = x.StatusModifiedDiff.GetValueOrDefault();
                            objProjectSummaryModel.ProjectLastUpdateString = (ProjectModifiedDiff <= MilestoneModifiedDiff && ProjectModifiedDiff <= StatusModifiedDiff) ? Convert.ToString(x.ProjectModifedDate) : (MilestoneModifiedDiff <= StatusModifiedDiff ? Convert.ToString(x.MilestoneModifiedDate) : Convert.ToString(x.StatusModifiedDate));
                            string TimestampDetail = x.TimestampModifiedDetail;
                            if(TimestampDetail != null)
                            {
                                string[] splitColumns = TimestampDetail.Trim(',').Split(',');
                                if(splitColumns != null && splitColumns.Length > 0)
                                {
                                    for (int i = 0; i < splitColumns.Length; i++)
                                    {
                                        string[] splitTime = splitColumns[i].Trim('~').Split('~');
                                        if (splitTime != null && splitTime.Length > 0)
                                        {
                                            if (splitTime[0].ToString().Trim() == "Name")
                                            {
                                                objProjectSummaryModel.ProjectNameModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "EOPlaybook")
                                            {
                                                objProjectSummaryModel.ETOPlayBookModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "PeerReview")
                                            {
                                                objProjectSummaryModel.PeerReviewModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ProjectOwnerANTMId")
                                            {
                                                objProjectSummaryModel.AnthemOwnerModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ProjectOwnerExternalId")
                                            {
                                                objProjectSummaryModel.ExternalOwnerModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "StartDate")
                                            {
                                                objProjectSummaryModel.ProjectStartDateModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "EndDate")
                                            {
                                                objProjectSummaryModel.ProjectEndDateModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "Complete")
                                            {
                                                objProjectSummaryModel.ProjectCompleteModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ScheduleId")
                                            {
                                                objProjectSummaryModel.ProjectScheduleModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ScopeId")
                                            {
                                                objProjectSummaryModel.ProjectScopeModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ResourcesId")
                                            {
                                                objProjectSummaryModel.ProjectResourceModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "FinancialId")
                                            {
                                                objProjectSummaryModel.ProjectFinancialModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ProjectStatusId")
                                            {
                                                objProjectSummaryModel.ProjectOverAllModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                            if (splitTime[0].ToString().Trim() == "ProjectNumber")
                                            {
                                                objProjectSummaryModel.ProjectNumberModifiedDate = Convert.ToString(splitTime[1]);
                                            }
                                          
                                        }
                                    }
                                }
                            }
                            else
                            {
                                objProjectSummaryModel.ProjectNumberModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                  objProjectSummaryModel.ProjectNameModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;
                               
                                    objProjectSummaryModel.ETOPlayBookModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;
                                
                                    objProjectSummaryModel.PeerReviewModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;
                                
                                    objProjectSummaryModel.AnthemOwnerModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;
                               
                                    objProjectSummaryModel.ExternalOwnerModifiedDate =objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectStartDateModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectEndDateModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectCompleteModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectScheduleModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectScopeModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectResourceModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectFinancialModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                                    objProjectSummaryModel.ProjectOverAllModifiedDate = objProjectSummaryModel.ProjectLastUpdateString;

                            }
                            
                            
                            
                            lstSummaryModel.Add(objProjectSummaryModel);

                        }
                    }

                    return lstSummaryModel;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static ProjectSummaryDetailModel GetProjectSummaryDetail(int projectId)
        {
            try
            {
                ProjectSummaryDetailModel objSummaryModel = null;
                using (DCSEntities context = new DCSEntities())
                {
                    var result = context.usp_GetProjectSummaryDetails2(projectId).FirstOrDefault();
                    if (result != null)
                    {
                        objSummaryModel = new ProjectSummaryDetailModel();
                        objSummaryModel.AnthemOwner = result.FullName;
                        objSummaryModel.PartnerName = result.PartnerName;
                        objSummaryModel.ETOPlayBookName = result.EtoPlayBookName;
                        objSummaryModel.ProjectAccomplishments = result.Accomplishments;
                        objSummaryModel.ProjectFundingAsk = result.FundingAsk;
                        objSummaryModel.ProjectComplete = result.ProjectComplete.HasValue ? result.ProjectComplete.Value : decimal.MinValue;
                        objSummaryModel.ProjectDescription = result.ProjectDescription;
                        objSummaryModel.ProjectDirection = result.Direction;
                        objSummaryModel.ProjectEndDateString = result.ProjectEndDate.HasValue ? result.ProjectEndDate.Value.ToShortDateString() : DateTime.MinValue.ToShortDateString();
                        objSummaryModel.ProjectExecutiveSummary = result.ExecutiveStatus;
                        objSummaryModel.ProjectId = result.ID;
                        objSummaryModel.ProjectKeyRiskIssues = result.KeyRisksAndIssues;
                        objSummaryModel.ProjectName = result.ProjectName;
                        objSummaryModel.ProjectStartDateString = result.ProjectStartDate.HasValue ? result.ProjectStartDate.Value.ToShortDateString() : DateTime.MinValue.ToShortDateString();
                        objSummaryModel.ProjectStartDate = result.ProjectStartDate.HasValue ? result.ProjectStartDate.Value : DateTime.MinValue;
                        objSummaryModel.ProjectEndDate = result.ProjectEndDate.HasValue ? result.ProjectEndDate.Value : DateTime.MinValue;
                        objSummaryModel.ScopeId = result.ScopeId.HasValue ? result.ScopeId.Value : int.MinValue;
                        objSummaryModel.ResourceId = result.ResourcesId.HasValue ? result.ResourcesId.Value : int.MinValue;
                        objSummaryModel.FinancialId = result.FinancialId.HasValue ? result.FinancialId.Value : int.MinValue;
                        objSummaryModel.OverallStatusId = result.ProjectStatusId.HasValue ? result.ProjectStatusId.Value : int.MinValue;
                        objSummaryModel.ScheduleId = result.ScheduleId.HasValue ? result.ScheduleId.Value : int.MinValue;

                        List<MileStone> lstMileStones = new List<MileStone>();
                        var resultMileStones = context.ProjectMilestones.Where(x => x.ProjectId == projectId).ToList();
                        if (resultMileStones != null && resultMileStones.Count > 0)
                        {
                            foreach (var mileStone in resultMileStones)
                            {
                                MileStone objMileStone = new MileStone();
                                objMileStone.Title = mileStone.Name;
                                objMileStone.MileStoneDate = mileStone.StartDate.HasValue ? mileStone.StartDate.Value : DateTime.MinValue;
                                objMileStone.DateString = mileStone.StartDate.HasValue ? string.Format("{0:MM/dd/yyyy}", mileStone.StartDate.Value.ToShortDateString()) : DateTime.MinValue.ToShortDateString();
                                objMileStone.MilestoneStatus = mileStone.MilestoneStatus.GetValueOrDefault();
                                lstMileStones.Add(objMileStone);
                            }
                        }
                        objSummaryModel.ProjectMileStones = lstMileStones;
                    }
                }
                return objSummaryModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
    }
}
