using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.Common.DTO
{
    /// <summary>
    /// Class MappedObjectAttribute. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class MappedObject : Attribute
    {
        /// <summary>
        /// The _mapped type
        /// </summary>
        readonly Type _mappedType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappedObjectAttribute"/> class.
        /// </summary>
        /// <param name="mappedType">Type to be mapped.</param>
        public MappedObject(Type mappedType)
        {
            this._mappedType = mappedType;
        }

        /// <summary>
        /// Gets the Type to be mapped.
        /// </summary>
        /// <value>The Type to be mapped.</value>
        public Type MappedType
        {
            get { return _mappedType; }
        }

        /// <summary>
        /// Gets or sets the primary identifier for this and the mapped type.
        /// </summary>
        /// <value>The primary identifier for this and the mapped type.</value>
        public string Key { get; set; }
    }

}
