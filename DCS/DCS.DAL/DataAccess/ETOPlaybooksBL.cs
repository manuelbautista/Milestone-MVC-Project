using DCS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace DCS.DAL.DataAccess
{
    public class ETOPlaybooksBL
    {
        public List<ETOPlaybookModel> GetUsingMapper()
        {
            using (DCSEntities context = new DCSEntities())
            {
                var list = context.ETOPlaybooks.ToList();

                return Mapper.Map<List<ETOPlaybook>, List<ETOPlaybookModel>>(list);
            }
        }
    }
}
