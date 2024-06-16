using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFSubCategoryDAL : EFRepositoryBase<SubCategory, AppDbContext>, ISubCategoryDAL
    {
        public void DeleteSubcategory(Guid Id)
        {
            var context = new AppDbContext();
            var SubCategory = context.SubCategories.SingleOrDefault(x => x.Id == Id);
            context.SubCategories.Remove(SubCategory);
            context.SaveChanges();

        }

        public async Task<SubCategory> GetByIdAsync(Guid id)
        {
            var context = new AppDbContext();
            return await context.Set<SubCategory>().FindAsync(id);
        }

        public async Task UpdateAsync(SubCategory subCategory)
        {
            var context = new AppDbContext();
            context.Set<SubCategory>().Update(subCategory);
            await context.SaveChangesAsync();
        }
    }
}
