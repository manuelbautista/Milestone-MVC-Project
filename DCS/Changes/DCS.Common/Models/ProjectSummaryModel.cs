using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.Common.Models
{
    public class ProjectSummaryModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string AnthemOwner { get; set; }
        public System.DateTime ProjectStartDate { get; set; }
        public System.DateTime ProjectEndDate { get; set; }
        public decimal ProjectComplete { get; set; }
        public string ProjectScope { get; set; }
        public string ProjectSchedule { get; set; }
        public string ProjectResource { get; set; }
        public string ProjectFinancial { get; set; }
        public string ProjectOverAll { get; set; }
        public string IsETOPlayBook { get; set; }
        public string IsPeerReview { get; set; }
        public System.DateTime ProjectLastUpdate { get; set; }
        public decimal ProjectEffortRemaining { get; set; }
        public decimal ProjectDaysRemaining { get; set; }
        public string ProjectRAGStatus { get; set; }
        public string ProjectStartDateString { get; set; }
        public string ProjectEndDateString { get; set; }
        public string ProjectLastUpdateString { get; set; }
        public string AnthemOwnerEmail { get; set; }
        public string ExternalOwner { get; set; }
        public string ExternalOwnerEmail { get; set; }
    }

    public class ProjectSummaryDetailModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string AnthemOwner { get; set; }
        public System.DateTime ProjectStartDate { get; set; }
        public System.DateTime ProjectEndDate { get; set; }
        public decimal ProjectComplete { get; set; }
        public string ProjectStartDateString { get; set; }
        public string ProjectEndDateString { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectExecutiveSummary { get; set; }
        public string ProjectAccomplishments { get; set; }
        public string ProjectKeyRiskIssues { get; set; }
        public string ProjectDirection { get; set; }
        public int NoOfMonths { get { return this.ProjectEndDate.Month - this.ProjectStartDate.Month; } }
        private string GetMonthNameWithYear(int month,string year)
        {
            string value = string.Empty;
            switch (month)
            {
                case 1:
                    value = "Jan '" + year;
                    break;
                case 2:
                    value = "Feb '" + year;
                    break;
                case 3:
                    value = "Mar '" + year;
                    break;
                case 4:
                    value = "Apr '" + year;
                    break;
                case 5:
                    value = "May '" + year;
                    break;
                case 6:
                    value = "Jun '" + year;
                    break;
                case 7:
                    value = "Jul '" + year;
                    break;
                case 8:
                    value = "Aug '" + year;
                    break;
                case 9:
                    value = "Sep '" + year;
                    break;
                case 10:
                    value = "Oct '" + year;
                    break;
                case 11:
                    value = "Nov '" + year;
                    break;
                case 12:
                    value = "Dec '" + year;
                    break;
            }
            return value;
        }
        private string GetMonthName(int month)
        {
            string value = string.Empty;
            switch (month)
            {
                case 1:
                    value = "Jan '" ;
                    break;
                case 2:
                    value = "Feb '" ;
                    break;
                case 3:
                    value = "Mar '" ;
                    break;
                case 4:
                    value = "April '" ;
                    break;
                case 5:
                    value = "May '" ;
                    break;
                case 6:
                    value = "Jun '" ;
                    break;
                case 7:
                    value = "Jul '" ;
                    break;
                case 8:
                    value = "Aug '" ;
                    break;
                case 9:
                    value = "Sep '" ;
                    break;
                case 10:
                    value = "Oct '";
                    break;
                case 11:
                    value = "Nov '" ;
                    break;
                case 12:
                    value = "Dec '" ;
                    break;
            }
            return value;
        }
        public List<ProjectDuration> ProjectDurationList
        {
            get
            {
                List<ProjectDuration> lstDuration = new List<ProjectDuration>();
                //Logic part
                int yearDiff = this.ProjectEndDate.Year - this.ProjectStartDate.Year;
                List<KeyValueObject> lstMonth = new List<KeyValueObject>();
                if (yearDiff > 0)
                {
                    for (int c = this.ProjectStartDate.Month; c <= 12; c++)
                    {
                        KeyValueObject tmpObject = new KeyValueObject();
                        tmpObject.Month = c;
                        tmpObject.Year = this.ProjectStartDate.Year.ToString();
                        lstMonth.Add(tmpObject);
                    }
                    for (int j = 1; j <= yearDiff; j++)
                    {
                        if (j == yearDiff)
                        {
                            for (int k = 1; k <= this.ProjectEndDate.Month; k++)
                            {
                                KeyValueObject tmpObject = new KeyValueObject();
                                tmpObject.Month = k;
                                tmpObject.Year = this.ProjectEndDate.Year.ToString();
                                lstMonth.Add(tmpObject);
                            }
                        }
                        else
                        {
                            for (int l = 1; l <= 12; l++)
                            {
                                KeyValueObject tmpObject = new KeyValueObject();
                                tmpObject.Month = l;
                                tmpObject.Year = (this.ProjectStartDate.Year + j).ToString();
                                lstMonth.Add(tmpObject);
                            }
                        }
                    }
                }
                else {
                    for (int i = this.ProjectStartDate.Month; i <= this.ProjectEndDate.Month; i++) {
                        KeyValueObject tmpObject = new KeyValueObject();
                        tmpObject.Month = i;
                        tmpObject.Year = this.ProjectStartDate.Year.ToString();
                        lstMonth.Add(tmpObject);
                    }
                }
                //logic part
                //
                for (int i = 0; i < lstMonth.Count; i++)
                {
                    ProjectDuration objDuration = new ProjectDuration();
                    objDuration.Title = GetMonthNameWithYear(lstMonth[i].Month, lstMonth[i].Year);
                    List<MileStone> objMileStonesList = new List<MileStone>();
                    if (this.ProjectMileStones != null && this.ProjectMileStones.Count > 0)
                    {
                        foreach (var milestone in this.ProjectMileStones)
                        {
                            if (lstMonth[i].Year == milestone.MileStoneDate.Year.ToString())
                            {
                                if (lstMonth[i].Month == milestone.MileStoneDate.Month)
                                {
                                    MileStone objMileStone = new MileStone();
                                    objMileStone.Title = milestone.Title;
                                    objMileStone.MileStoneDate = milestone.MileStoneDate;
                                    objMileStone.DateString = milestone.DateString;
                                    objMileStone.MileStoneDateNumber = milestone.MileStoneDate.Day;
                                    objMileStone.MilestoneStatus = milestone.MilestoneStatus;
                                    objMileStonesList.Add(objMileStone);
                                }
                            }
                        }
                    }
                    objDuration.MileStoneList = objMileStonesList;
                    lstDuration.Add(objDuration);
                }
                return lstDuration;
            }
        }

        public List<MileStone> ProjectMileStones { get; set; }

        public int ScopeId { get; set; }
        public int ResourceId { get; set; }
        public int FinancialId { get; set; }
        public int OverallStatusId { get; set; }
        public int ScheduleId { get; set; }
    }

    public class ProjectDuration
    {
        public string Title { get; set; }
        public List<MileStone> MileStoneList { get; set; }

    }

    public class MileStone
    {
        public string Title { get; set; }
        public string DateString { get; set; }
        public DateTime MileStoneDate { get; set; }
        public int MileStoneDateNumber { get; set; }

        public int MilestoneStatus { get; set; }

    }
    public class KeyValueObject
    {
        public int Month { get; set; }
        public string Year { get; set; }
    }
}
