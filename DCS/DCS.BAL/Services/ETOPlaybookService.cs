using DCS.Common.Models;
using DCS.DAL;
using DCS.BAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.BAL.Services
{
    public class ETOPlaybookService : Repository<ETOPlaybook, ETOPlaybookModel>
    {
        //private IRepository<ETOPlaybook> _ETOPlaybook;
        
        ////ETOPlaybooksBL objbl = new ETOPlaybooksBL();
        //public ETOPlaybookService()
        //{
        //    _ETOPlaybook = new Repository<ETOPlaybook>();
        //}

        ////public override ICollection<ETOPlaybook> GetAll()
        ////{
        ////    return base.GetAll();
        ////}

        ////public List<ETOPlaybookModel> Get()
        ////{
        ////    return objbl.GetUsingMapper();
        ////}
    }
}
