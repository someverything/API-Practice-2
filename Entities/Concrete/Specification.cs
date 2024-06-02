using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Specification : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<SpecificationLang> SpecificationLangs { get; set; }

    }
}
