using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SectionsController : ControllerBase
	{

		private readonly ISectionRepository _sectionRepository;

		public SectionsController(ISectionRepository sectionRepository)
		{
			_sectionRepository = sectionRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetSections()
		{
			var records = await _sectionRepository.GetAllAsync();
			return Ok(records);
		}

		[HttpGet("~/api/categories/{categoryId}/sections")]
		public async Task<IActionResult> GetSectionsByCategory(int categoryId)
		{
			var records = await _sectionRepository.GetSectionsByCategoryAsync(categoryId);
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSectionById([FromRoute] int id) 
		{ 
			var section = await _sectionRepository.GetByIdAsync(id);
			if (section == null) { return NotFound(); }
			return Ok(section);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddSection([FromBody] SectionModel sectionModel)
		{
			sectionModel.Id = await _sectionRepository.AddAsync(sectionModel);
			return CreatedAtAction(nameof(GetSectionById), new { id = sectionModel.Id }, sectionModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSection([FromRoute] int id, [FromBody] SectionModel sectionModel)
		{
			var result = await _sectionRepository.UpdateAsync(id, sectionModel);
			if (result != 1) { return BadRequest(); }
			return Ok(sectionModel);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchDocument)
		{
			var result = await _sectionRepository.UpdatePatchAsync(id, jsonPatchDocument);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _sectionRepository.DeleteAsync(id);
			return Ok();
		}
	}
}
