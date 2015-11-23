using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCS.DAL;
using DCS.Common.Models;
using System.Data.SqlClient;

namespace DCS.BAL.DataAccess
{
    public class ProjectMileStone
    {
    


        public static List<ProjectMilestoneModel> GetProjectMileStones(int projectId)
        {
            try
            {
                List<ProjectMilestoneModel> lstMileStoneModel = new List<ProjectMilestoneModel>();
                using (DCSEntities db = new DCSEntities())
                {


                    var result = db.Database.SqlQuery<ProjectMilestoneModel>("usp_GetProjectMileStones @ProjectId", new SqlParameter("@ProjectId", projectId)).ToList();
                    if (result != null && result.Count > 0)
                    {
                        foreach (var x in result)
                        {
                            ProjectMilestoneModel objProjectMileStoneModel = new ProjectMilestoneModel();
                            objProjectMileStoneModel.ID = x.ID;
                            objProjectMileStoneModel.Name = x.Name;
                            objProjectMileStoneModel.StartDate = x.StartDate;
                            objProjectMileStoneModel.Complete = x.Complete;
                            objProjectMileStoneModel.MilestoneStatus = x.MilestoneStatus;
                            lstMileStoneModel.Add(objProjectMileStoneModel);
                        }
                    }
                    return lstMileStoneModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SaveProjectMileStone(ProjectMilestoneModel objProjectMileStone)
        {
            try
            {
                using (DCSEntities db = new DCSEntities())
                {
                    List<object> paramObject = new List<object>();
                    paramObject.Add(new SqlParameter("@MileStoneId", objProjectMileStone.ID));
                    paramObject.Add(new SqlParameter("@ProjectId", objProjectMileStone.ProjectId));
                    paramObject.Add(new SqlParameter("@Name", objProjectMileStone.Name));
                    paramObject.Add(new SqlParameter("@StartDate", objProjectMileStone.StartDate));
                    paramObject.Add(new SqlParameter("@Complete", objProjectMileStone.Complete));
                    paramObject.Add(new SqlParameter("@MilestoneStatus", objProjectMileStone.MilestoneStatus));
                   // paramObject.Add(new SqlParameter("@ModifiedDate", objProjectMileStone.ModifiedDate));

                    var result = db.Database.ExecuteSqlCommand("usp_SaveProjectMileStone @MileStoneId,@ProjectId,@Name,@StartDate,@Complete,@MilestoneStatus", paramObject.ToArray());
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool RemoveProjectMileStone(ProjectMilestoneModel objProjectMileStone)
        {
            try
            {
                using (DCSEntities db = new DCSEntities())
                {
                    List<object> paramObject = new List<object>();
                    paramObject.Add(new SqlParameter("@MileStoneId", objProjectMileStone.ID));
                    paramObject.Add(new SqlParameter("@ProjectId", objProjectMileStone.ProjectId));
                    var result = db.Database.ExecuteSqlCommand("usp_RemoveProjectMileStone @MileStoneId,@ProjectId", paramObject.ToArray());
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class ProjectDetail
    {

        public static bool RemoveProject(int projectId)
        {
            try
            {
                using (DCSEntities context = new DCSEntities())
                {
                    SqlParameter prmProjectId = new SqlParameter("@ProjectId", projectId);
                    var result = context.Database.ExecuteSqlCommand("usp_RemoveProject @ProjectId", prmProjectId);
                    return result > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveOwner(int ownerId)
        {
            string status = string.Empty;
            try
            {
                using (DCSEntities context = new DCSEntities())
                {
                    ProjectOwnerModel model = new ProjectOwnerModel();
                    SqlParameter prmProjectId = new SqlParameter("@OwnerId", ownerId);
                    var result = context.Database.SqlQuery<ProjectOwnerModel>("usp_RemoveOwner @OwnerId", prmProjectId).SingleOrDefault();
                    if (result != null)
                    {
                        status = result.Status;
                    }
                    return status;
                }
            }
            catch (Exception ex)
            {
                return status;
            }
        }
     
    }
}
