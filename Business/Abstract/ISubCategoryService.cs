using Core.Utilities.Results.Abstract;
using Entities.DTOs.SubCategoryDTOs;

namespace Business.Abstract
{
    public interface ISubCategoryService
    {
        IResult Create(AddSubCategoryDTO model);
        Task<IResult> UpdateAsync(Guid Id, UpdateSubCategoryDTO model);
        void Delete(Guid Id);
        Task<IDataResult<SubCategoeyDTO>> GetSubCategory(Guid Id);
    }
}
