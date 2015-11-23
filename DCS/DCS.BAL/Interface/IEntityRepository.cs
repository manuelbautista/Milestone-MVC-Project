using DCS.Common.Interface;
using DCS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCS.BAL.Interface
{

    public interface IODataRepository<TEntity> : IBaseOdataRepository<TEntity>
        where TEntity : class        
    {

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        //TEntity GetSingleIncluding(TId id, params Expression<Func<TEntity, object>>[] includeProperties);
        void Add(TEntity entity);
        void AddGraph(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        //TEntity Find(TId id);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

    }

    //public interface IEntityRepository<TEntity> : IODataRepository<TEntity, int>
    //   where TEntity : class, IEntity<int>
    //{

    //}


    public interface IBaseOdataRepository<TEntity>
        where TEntity : class
    {

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        //TEntity GetSingle(TId id);
    }

    //public interface IBaseOdataRepository<TEntity> : IBaseOdataRepository<TEntity, int>
    //   where TEntity : class, IEntity<int>
    //{
    //}

}
