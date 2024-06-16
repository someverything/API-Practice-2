using Entities.Common;

namespace Entities.Concrete
{
    public class ProductLang : BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
