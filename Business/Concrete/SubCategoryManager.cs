using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.SubCategoryDTOs;

namespace Business.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;

        public SubCategoryManager(ISubCategoryDAL subCategoryDAL)
        {
            _subCategoryDAL = subCategoryDAL;
        }

        public IResult Create(AddSubCategoryDTO model)
        {
            try
            {

                _subCategoryDAL.Add(new()
                {
                    Name = model.Name,
                    CreatedDate = DateTime.Now,
                    CategoryId = model.CategoryId,
                });
                return new SuccessResult("Ugurla Elave Olundu!", System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public void Delete(Guid Id)
        {
            _subCategoryDAL.DeleteSubcategory(Id);
        }

        public async Task<IDataResult<SubCategoeyDTO>> GetSubCategory(Guid Id)
        {
            try
            {
                var subCategory = await _subCategoryDAL.GetByIdAsync(Id);
                if (subCategory == null)
                {
                    return new ErrorDataResult<SubCategoeyDTO>("Subcategory not found", System.Net.HttpStatusCode.NotFound);
                }

                var subCategoryDTO = new SubCategoeyDTO
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    CategoryId = subCategory.CategoryId,
                };

                return new SuccessDataResult<SubCategoeyDTO>(subCategoryDTO, "Subcategory retrieved successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SubCategoeyDTO>(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateAsync(Guid Id, UpdateSubCategoryDTO model)
        {
            try
            {
                var existingSubCategory = await _subCategoryDAL.GetByIdAsync(Id);
                if (existingSubCategory == null)
                {
                    return new ErrorResult("Subcategory not found", System.Net.HttpStatusCode.NotFound);
                }

                existingSubCategory.Name = model.Name;
                existingSubCategory.UpdatedDate = DateTime.Now;

                await _subCategoryDAL.UpdateAsync(existingSubCategory);
                return new SuccessResult("Successfully updated", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }

        }


    }
}
