using DCS.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DCS.BAL.Interface
{
    public interface IRepository<TEntity, TModel> : IDisposable
    {
        //IDbSet<TEntity> Set<TEntity>();
        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        TModel Get(Expression<Func<TEntity, bool>> filter);

        ICollection<TModel> GetAll();

        IQueryable<TEntity> GetAllList();

        bool UpdateProject(DCS.Common.Models.ProjectModel model);

        int GetTopOwnerId();

        IEnumerable<GetAllProjectsIndex_Result> GetProjects();

        bool UpdateETOPlaybook(DCS.Common.Models.ETOPlaybookModel model);

        int GetTopETOPlaybookId();

        TEntity GetEntity(Expression<Func<TEntity, bool>> filter);

        GetDetailForProject_Result GetProjectById(int ProjectId);
        bool UpdateProjectStatus(DCS.Common.Models.ProjectStatusModel model);

        int GetTopProjectId();

        bool UpdateProjectSchedule(string StartDate, string EndDate, int ProjectID,decimal percentageComplete);

        bool IsProjectNumberExist(string projectNumber);


        List<usp_GetProjectMultiEmail_Result1> GetProjectMultiEmail(int ProjectId);

        usp_RemoveMultiEmail1_Result1 RemoveProjectMultiEmail(int ID);

        Nullable<Decimal> GetProjectPercentageCompleteByProjectId(int ProjectId);

    }
}
