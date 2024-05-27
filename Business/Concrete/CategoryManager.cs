using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        private readonly ICategoryLangDAL _categorylangDAL;

        public CategoryManager(ICategoryDAL categoryDAL, ICategoryLangDAL categorylangDAL)
        {
            _categoryDAL = categoryDAL;
            _categorylangDAL = categorylangDAL;
        }

        public void Create(List<AddCategoryDTO> models)
        {
            _categoryDAL.CreateCategory(models);
        }

        public void Delete(Guid Id)
        {
            _categoryDAL.DeleteCategory(Id);
        }

        public GetCategoryDTO GetByLang(Guid Id, string langCode)
        {
            var category = _categorylangDAL.Get(x => x.CategoryId == Id && x.LangCode == langCode);
            GetCategoryDTO getCategoryDTO = new()
            {
                Name = category.Name,
                CategoryId = Id,
                LangCode = langCode
            }; 

            return getCategoryDTO;
        }

        public async Task Update(Guid Id, List<UpdateCategoryDTO> models)
        {
            await _categoryDAL.UpdateCategoryAsync(Id, models);
        }
    }
}
