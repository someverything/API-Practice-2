using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public void CreateCategory(List<AddCategoryDTO> models)
        {
            using var context = new AppDbContext();
            Category category = new();
            context.Categories.Add(category);
            context.SaveChanges();

            for (int i = 0; i < models.Count; i++)
            {
                CategoryLanguage categoryLanguage = new()
                {
                    Name = models[i].Name,
                    LangCode = models[i].LangCode,
                    CategoryId = category.Id
                };
                context.CategoryLanguages.Add(categoryLanguage);

            }
            context.SaveChanges();
        }

        public void DeleteCategory(Guid Id)
        {
            using var context = new AppDbContext();
            var category = context.Categories.FirstOrDefault(c => c.Id == Id);
            var categoryLang = context.CategoryLanguages.Where(c => c.CategoryId == Id);
            context.Categories.Remove(category);
            context.CategoryLanguages.RemoveRange(categoryLang);
            context.SaveChanges();
        }

        public GetCategoryDTO GetCategoryByLang(Guid Id, string LangCode)
        {
            using var context = new AppDbContext();
            var categories = context.CategoryLanguages.FirstOrDefault(c => c.CategoryId == Id && c.LangCode == LangCode);
            GetCategoryDTO GetCategoryDTO = new()
            {
                Name = categories.Name,
                LangCode = LangCode,
                CategoryId = Id
            };
            return GetCategoryDTO;
        }

        public async Task UpdateCategoryAsync(Guid Id, List<UpdateCategoryDTO> models)
        {
            using var context = new AppDbContext();

            var category = context.Categories.FirstOrDefault(x => x.Id == Id);
            category.UpdatedDate = DateTime.Now;
            context.Categories.Update(category);
            var categoryLanguage = context.CategoryLanguages
                .Where(x => x.CategoryId == category.Id).ToList();

            context.CategoryLanguages.RemoveRange(categoryLanguage);

            await context.SaveChangesAsync();
            for (int i = 0; i < models.Count; i++)
            {
                CategoryLanguage newCategoryLang = new()
                {
                    Name = models[i].Name,
                    LangCode = models[i].LangCode,
                    CategoryId = category.Id
                };
                await context.CategoryLanguages.AddAsync(newCategoryLang);
            }
            await context.SaveChangesAsync();

        }

    }
}
