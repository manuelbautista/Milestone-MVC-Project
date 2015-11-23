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
   public class EToPlayBook
    {
        public static List<ETOPlaybookModel> GetETOPlayBookMileStones()
        {
            try
            {
                List<ETOPlaybookModel> lstMileStoneModel = new List<ETOPlaybookModel>();
                using (DCSEntities db = new DCSEntities())
                {


                    var result = db.Database.SqlQuery<ETOPlaybookModel>("usp_GetETOPlayBookMileStones").ToList();
                    if (result != null && result.Count > 0)
                    {
                        foreach (var x in result)
                        {
                            ETOPlaybookModel objETOPlaybookMileStoneModel = new ETOPlaybookModel();
                            objETOPlaybookMileStoneModel.ID = x.ID;
                            objETOPlaybookMileStoneModel.Name = x.Name;
                            objETOPlaybookMileStoneModel.IsActive = x.IsActive;
                            lstMileStoneModel.Add(objETOPlaybookMileStoneModel);
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
    }
    
}
