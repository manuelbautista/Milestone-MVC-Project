using AutoMapper;
using DCS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.DAL.DataAccess
{
    public class Repository<TEntity, TModel> : IDisposable
        where TEntity : class
        where TModel : class, new()
    {
        protected DCSEntities _context;

        protected Repository()
        {
            _context = new DCSEntities();
        }

        /// <summary>
        /// Get all the items in entity in form of a list using mapping
        /// </summary>
        /// <returns>List of all TEntity Items</returns>
        public virtual List<TModel> GetAll()
        {
            return Mapper.Map<List<TEntity>, List<TModel>>(_context.Set<TEntity>().ToList());
        }

        public virtual TModel GetSingle()
        {
            return Mapper.Map<TEntity, TModel>(_context.Set<TEntity>().FirstOrDefault());
        }

        public virtual void Add(TEntity entity)
        {

        }

        public virtual void Update(TEntity entity)
        {

        }

        public virtual void Delete(TEntity entity)
        {

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
        public void Dispose()
        {
            if (!disposed)
            {
                _context.Dispose();
                disposed = true;
            }

        }
    }
}
