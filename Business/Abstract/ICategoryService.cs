using Entities.DTOs.CategoryDTOs;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void Create(List<AddCategoryDTO> models);
        Task Update(Guid Id, List<UpdateCategoryDTO> models);
        void Delete(Guid Id);
        GetCategoryDTO GetByLang(Guid Id, string langCode);
    }
}
