using DCS.Common.Models;
using DCS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.BAL
{
    public class AutoMap
    {
        public static void Map()
        {
            BaseAutoMap.Configure();
        }
    }
}
