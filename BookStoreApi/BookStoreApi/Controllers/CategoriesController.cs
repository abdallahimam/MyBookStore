using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{

		private readonly ICategoryRepository _categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetCategories()
		{
			var records = await _categoryRepository.GetAllAsync();
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] int id) 
		{ 
			var category = await _categoryRepository.GetByIdAsync(id);
			if (category == null) { return NotFound(); }
			return Ok(category);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddCategory([FromBody] CategoryModel categoryModel)
		{
			categoryModel.Id = await _categoryRepository.AddAsync(categoryModel);
			return CreatedAtAction(nameof(GetCategoryById), new { id = categoryModel.Id }, categoryModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryModel categoryModel)
		{
			var result = await _categoryRepository.UpdateAsync(id, categoryModel);
			if (result != 1) { return BadRequest(); }
			return Ok(categoryModel);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchDocument)
		{
			var result = await _categoryRepository.UpdatePatchAsync(id, jsonPatchDocument);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _categoryRepository.DeleteAsync(id);
			return Ok();
		}
	}
}
