using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Create(List<AddCategoryDTO> models)
        {
            _categoryService.Create(models);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, List<UpdateCategoryDTO> models)
        { 
            await _categoryService.Update(Id, models);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id) { 
            _categoryService.Delete(Id);
            return Ok();
        }
            
        [HttpGet("{Id}")]
        public IActionResult Get(Guid Id) {
            string langCode = Request.Headers.AcceptLanguage;
            if (langCode != "az" || langCode != "ru-RU" || langCode != "en-US") {
                var res = _categoryService.GetByLang(Id, "az");
                return Ok(res);
            }
            var result = _categoryService.GetByLang(Id, langCode);
            return Ok(result);
        }
    }
}

//50667759 - bf4a - 42ba - d85e - 08dc7bf0b7c3
