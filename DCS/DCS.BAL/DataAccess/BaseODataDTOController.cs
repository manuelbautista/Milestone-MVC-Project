using AutoMapper;
using AutoMapper.QueryableExtensions;
using DCS.BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;

namespace DCS.BAL.DataAccess
{
    public class BaseODataDTOController<TEntity, TDTO> : ODataController
        where TEntity : class, IEntity<int>
        where TDTO : class
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        protected IEntityRepository<TEntity> _genericRepository;

        public BaseODataDTOController(IEntityRepository<TEntity> repository)
        {
            _genericRepository = repository;
        }

        [EnableQuery]
        public virtual IQueryable<TDTO> Get()
        {
            return _genericRepository.GetAllList().Project().To<TDTO>();
        }

        [EnableQuery]
        public virtual SingleResult<TDTO> Get([FromODataUri] int key)
        {
            return SingleResult.Create(new List<TDTO> { Mapper.Map<TDTO>(_genericRepository.Find(key)) }.AsQueryable());
        }

        public virtual async System.Threading.Tasks.Task<IHttpActionResult> PutObject([FromODataUri] int key, Delta<TDTO> delta, CancellationToken cancellationToken)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _genericRepository.FindAsync(cancellationToken, key);
            var dto = Mapper.Map<TDTO>(entity);
            delta.Put(dto);
            Mapper.Map(dto, entity);
            await _genericRepository.SaveChangesAsync(cancellationToken);
            return Updated(dto);
        }

        public virtual async System.Threading.Tasks.Task<IHttpActionResult> Put([FromODataUri] int key, TDTO dto, CancellationToken cancellationToken)
        {
            Validate(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _genericRepository.FindAsync(key);
            Mapper.Map(dto, entity);
            await _genericRepository.SaveChangesAsync();
            return Updated(dto);
        }

        public virtual async System.Threading.Tasks.Task<IHttpActionResult> Post(TDTO dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<TEntity>(dto);
            _genericRepository.Add(entity);
            await _genericRepository.SaveChangesAsync(cancellationToken);
            return Created(Mapper.Map<TDTO>(entity));
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async System.Threading.Tasks.Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TDTO> delta, CancellationToken cancellationToken)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _genericRepository.FindAsync(cancellationToken, key);
            var dto = Mapper.Map<TDTO>(entity);
            delta.Patch(dto);
            Mapper.Map(dto, entity);
            await _genericRepository.SaveChangesAsync(cancellationToken);
            return Updated(dto);
        }

        public virtual async System.Threading.Tasks.Task<IHttpActionResult> Delete([FromODataUri] int key, CancellationToken cancellationToken)
        {
            var entity = await _genericRepository.FindAsync(cancellationToken, key);
            _genericRepository.Delete(entity);
            await _genericRepository.SaveChangesAsync(cancellationToken);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
