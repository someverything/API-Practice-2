using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
