using Entities.Common;

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
