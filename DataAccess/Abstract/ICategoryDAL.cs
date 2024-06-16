using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace DataAccess.Abstract
{
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        void CreateCategory(List<AddCategoryDTO> models);
        Task UpdateCategoryAsync(Guid Id, List<UpdateCategoryDTO> models);
        void DeleteCategory(Guid Id);
        GetCategoryDTO GetCategoryByLang(Guid Id, string LangCode);

    }
}
