using Application.DTOs.Category;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiControllerBase<Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Response<CategoryGetDTO>>> Create([FromForm] CategoryCreateDTO category)
        {
            Category mappedCategory = _mapper.Map<Category>(category);
            await _categoryRepository.CreateAsync(mappedCategory);
            CategoryGetDTO categoryGetDTO = _mapper.Map<CategoryGetDTO>(mappedCategory);
            return Ok(new Response<CategoryGetDTO>(categoryGetDTO));
        }
    }
}
