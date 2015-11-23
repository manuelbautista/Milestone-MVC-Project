using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCS.Common.DTO;
using DCS.DAL;
using DCS.BAL.DataAccess;
using DCS.BAL.Interface;
using System.Web.Mvc;
using DCSProject.OData;
using DCS.Common.Models;
using System.Web.OData;
using System.Web.Http;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net;


namespace DCSProject.Controllers
{
    public class ETOPlaybookDTOController : ODataController //: BaseODataDTOController<ETOPlaybook, ETOPlaybookDTO>
    {
        DCSEntities _context;
        public ETOPlaybookDTOController()
        //: base(DependencyResolver.Current.GetService<IODataRepository<ETOPlaybook>>())
        {
            _context = new DCSEntities();

        }
        //[EnableQuery]
        //public virtual IQueryable<ETOPlaybookDTO> Get()
        //{
        //    return Mapper.Map<List<ETOPlaybook>, List<ETOPlaybookDTO>>(_context.Set<ETOPlaybook>().ToList()) as IQueryable<ETOPlaybookDTO>;
        //    //return _context.Set<ETOPlaybook>().ToList().Project().To<ETOPlaybookDTO>();
        //}

        [EnableQuery]
        public IQueryable<ETOPlaybookDTO> Get()
        {
            return _context.ETOPlaybooks.Project().To<ETOPlaybookDTO>();
        }

        [EnableQuery]
        public SingleResult<ETOPlaybookDTO> Get([FromODataUri] int key)
        {
            IQueryable<ETOPlaybook> result = _context.ETOPlaybooks.Where(p => p.ID == key);
            return SingleResult.Create(new List<ETOPlaybookDTO> { Mapper.Map<ETOPlaybookDTO>(result) }.AsQueryable());
        }

        public async Task<IHttpActionResult> Post(ETOPlaybook etoPlaybook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.ETOPlaybooks.Add(etoPlaybook);
            await _context.SaveChangesAsync();
            return Created(etoPlaybook);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ETOPlaybook> product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ETOPlaybooks.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            product.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ProductExists(key))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }
            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int key, ETOPlaybook etoPlaybook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != etoPlaybook.ID)
            {
                return BadRequest();
            }
            _context.Entry(etoPlaybook).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ProductExists(key))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }
            return Updated(etoPlaybook);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var product = await _context.ETOPlaybooks.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            _context.ETOPlaybooks.Remove(product);
            await _context.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}