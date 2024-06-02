using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISubCategoryDAL : IRepositoryBase<SubCategory>
    {
        Task UpdateAsync(SubCategory subCategory);
        Task<SubCategory> GetByIdAsync(Guid id);
        void DeleteSubcategory(Guid Id);
    }
}
