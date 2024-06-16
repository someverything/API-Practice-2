using Entities.Common;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        public ICollection<CategoryLanguage> CategoryLanguages { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }


    }
}
