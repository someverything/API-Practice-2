using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ISubCategoryDAL : IRepositoryBase<SubCategory>
    {
        Task UpdateAsync(SubCategory subCategory);
        Task<SubCategory> GetByIdAsync(Guid id);
        void DeleteSubcategory(Guid Id);
    }
}
