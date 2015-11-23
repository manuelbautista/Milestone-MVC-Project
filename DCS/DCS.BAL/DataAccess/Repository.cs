using AutoMapper;
using DCS.Common.Models;
using DCS.DAL;
using DCS.BAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DCS.BAL.DataAccess
{
    public class Repository<TEntity, TModel> : IRepository<TEntity, TModel>, IDisposable
        where TEntity : class
        where TModel : class , new()
    {
        protected DCSEntities _context;

        public Repository()
        {
            _context = new DCSEntities();
        }

        public virtual TModel Get(Expression<Func<TEntity, bool>> filter)
        {
            return Mapper.Map<TEntity, TModel>(_context.Set<TEntity>().SingleOrDefault(filter));
        }

        public virtual TEntity GetEntity(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().Where(filter).FirstOrDefault();
        }

        public virtual ICollection<TModel> GetAll()
        {
            return Mapper.Map<List<TEntity>, List<TModel>>(_context.Set<TEntity>().ToList());

        }

        public virtual IQueryable<TEntity> GetAllList()
        {
            return _context.Set<TEntity>();
        }

        public virtual bool Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return true;
        }

        public virtual bool Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity); // (or context.Entity.Attach(entity);)
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public virtual List<usp_GetProjectMultiEmail_Result1> GetProjectMultiEmail(int ProjectId)
        {
            return _context.usp_GetProjectMultiEmail1(ProjectId).ToList();
        }


        public virtual IEnumerable<GetAllProjectsIndex_Result> GetProjects()
        {
            return _context.GetAllProjectsIndex().ToList();
        }

        public virtual GetDetailForProject_Result GetProjectById(int ProjectId)
        {
            return _context.GetDetailForProject(ProjectId).SingleOrDefault();
        }

        public Nullable<Decimal> GetProjectPercentageCompleteByProjectId(int ProjectId)
        {
            return _context.usp_GetProjectPercentageCompleteByProjectId(ProjectId).SingleOrDefault();
        }

        public bool UpdateProject(ProjectModel model)
        {
            try
            {
                Project project = (from pr in _context.Projects
                                   where pr.ID == model.ID
                                   select pr).FirstOrDefault<Project>();
                if (project.Name != model.Name)
                {
                    UpdateTimestamp(model.ID, "Name");
                }
                if (project.EOPlaybook != model.EOPlaybook)
                {
                    UpdateTimestamp(model.ID, "EOPlaybook");
                }
                if (project.PeerReview != model.PeerReview)
                {
                    UpdateTimestamp(model.ID, "PeerReview");
                }
                if (project.ProjectOwnerANTMId != model.ProjectOwnerANTMId)
                {
                    UpdateTimestamp(model.ID, "ProjectOwnerANTMId");
                }
                if (project.ProjectOwnerExternalId != model.ProjectOwnerExternalId)
                {
                    UpdateTimestamp(model.ID, "ProjectOwnerExternalId");
                }
                project.Name = model.Name;
                project.EOPlaybook = model.EOPlaybook;
                project.ManagementApproval = model.ManagementApproval;
                project.PeerReview = model.PeerReview;
                project.EOPlaybookId = model.EOPlaybookId;
                project.ProjectOwnerANTMId = model.ProjectOwnerANTMId;
                project.ProjectOwnerExternalId = model.ProjectOwnerExternalId;
                project.Description = model.Description;
                project.ExecutiveStatus = model.ExecutiveStatus;
                project.Accomplishments = model.Accomplishments;
                project.FundingAsk = model.FundingAsk;
                project.KeyRisksAndIssues = model.KeyRisksAndIssues;
                project.Direction = model.Direction;
                project.Modified = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return false;
            }
        }

        public bool UpdateProjectSchedule(string StartDate, string EndDate, int ProjectID,decimal percentageComplete)
        {
            try
            {
                ProjectSchedule projectSchedule = (from pr in _context.ProjectSchedules
                                           where pr.ProjectId == ProjectID
                                           select pr).FirstOrDefault<ProjectSchedule>();
                if (projectSchedule.StartDate != Convert.ToDateTime(StartDate))
                {
                    UpdateTimestamp(ProjectID, "StartDate");
                }
                if (projectSchedule.EndDate != Convert.ToDateTime(EndDate))
                {
                    UpdateTimestamp(ProjectID, "EndDate");
                }
                if (projectSchedule.Complete != percentageComplete)
                {
                    UpdateTimestamp(ProjectID, "Complete");
                }
                projectSchedule.StartDate = Convert.ToDateTime(StartDate);
                projectSchedule.EndDate = Convert.ToDateTime(EndDate);
                projectSchedule.Complete = percentageComplete;
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProjectStatus(ProjectStatusModel model)
        {
            try
            {
                ProjectStatu project = (from pr in _context.ProjectStatus
                                        where pr.ProjectId == model.ProjectId
                                        select pr).FirstOrDefault<ProjectStatu>();
                if (project.ScheduleId != model.ScheduleId)
                {
                    UpdateTimestamp(model.ProjectId, "ScheduleId");
                }
                if (project.ScopeId != model.ScopeId)
                {
                    UpdateTimestamp(model.ProjectId, "ScopeId");
                }
                if (project.ResourcesId != model.ResourcesId)
                {
                    UpdateTimestamp(model.ProjectId, "ResourcesId");
                }
                if (project.FinancialId != model.FinancialId)
                {
                    UpdateTimestamp(model.ProjectId, "FinancialId");
                }
                if (project.ProjectStatusId != model.ProjectStatusId)
                {
                    UpdateTimestamp(model.ProjectId, "ProjectStatusId");
                }
                project.ScheduleId = model.ScheduleId;
                project.ScopeId = model.ScopeId;
                project.ResourcesId = model.ResourcesId;
                project.FinancialId = model.FinancialId;
                project.ProjectStatusId = model.ProjectStatusId;
                project.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return false;
            }
        }

        public void UpdateTimestamp(int ProjectId, string ColumnName)
        {
            var result = _context.TimestampDetails.Where(v => v.ProjectId == ProjectId && v.ColumnName == ColumnName).FirstOrDefault();
            if (result != null)
            {
                result.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                TimestampDetail insertdetail = new TimestampDetail();
                insertdetail.ProjectId = ProjectId;
                insertdetail.ColumnName = ColumnName;
                insertdetail.ModifiedDate = DateTime.Now;
                _context.TimestampDetails.Add(insertdetail);
                _context.SaveChanges();
            }
        }



        public bool UpdateETOPlaybook(ETOPlaybookModel model)
        {
            try
            {
                ETOPlaybook project = (from et in _context.ETOPlaybooks
                                       where et.ID == model.ID
                                       select et).FirstOrDefault<ETOPlaybook>();
                project.Name = model.Name;
                project.Modified = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return false;
            }
        }


        public usp_RemoveMultiEmail1_Result1 RemoveProjectMultiEmail(int ID)
        {             
                return _context.usp_RemoveMultiEmail1(ID).FirstOrDefault();                
             
        }

        public int GetTopOwnerId()
        {
            return _context.ProjectOwners.OrderByDescending(o => o.ID).Select(ab => ab.ID).FirstOrDefault();
        }


        public int GetTopProjectId()
        {
            return _context.Projects.OrderByDescending(o => o.ID).Select(ab => ab.ID).FirstOrDefault();
        }

        public int GetTopETOPlaybookId()
        {
            return _context.ETOPlaybooks.OrderByDescending(o => o.ID).Select(ab => ab.ID).FirstOrDefault();
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsProjectNumberExist(string projectNumber)
        {
            var result=_context.Projects.Where(x => x.ProjectNumber.Equals(projectNumber)).FirstOrDefault();
            return result != null ? true : false;
        }
    }
}
