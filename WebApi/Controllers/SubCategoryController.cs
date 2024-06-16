using Business.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost]
        public IActionResult Create(AddSubCategoryDTO model)
        {
            var result = _subCategoryService.Create(model);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, UpdateSubCategoryDTO model)
        {
            await _subCategoryService.UpdateAsync(Id, model);
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            _subCategoryService.Delete(Id);
            return Ok();
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var res = await _subCategoryService.GetSubCategory(Id);
            return Ok(res);
        }
    }
}
