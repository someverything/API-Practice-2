using Entities.Common;

namespace Entities.Concrete
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
