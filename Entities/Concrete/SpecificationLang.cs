using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SpecificationLang : BaseEntity
    {
        public Guid SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
