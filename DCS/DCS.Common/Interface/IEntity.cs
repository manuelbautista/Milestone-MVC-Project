using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.Common.Interface
{
    public interface IEntity<TId> where TId : IComparable
    {

        TId ID { get; set; }
    }

    public interface IEntity : IEntity<int>
    {

    }

}
