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

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
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
            var result = _categoryDAL.GetCategoryByLang(Id, langCode);
            return result;
        }

        public async Task Update(Guid Id, List<UpdateCategoryDTO> models)
        {
            await _categoryDAL.UpdateCategoryAsync(Id, models);
        }
    }
}
