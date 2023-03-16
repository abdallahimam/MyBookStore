using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LanguagesController : ControllerBase
	{

		private readonly ILanguageRepository _languageRepository;

		public LanguagesController(ILanguageRepository languageRepository)
		{
			_languageRepository = languageRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetLanguages()
		{
			var records = await _languageRepository.GetAllAsync();
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetLanguageById([FromRoute] int id) 
		{ 
			var language = await _languageRepository.GetByIdAsync(id);
			if (language == null) { return NotFound(); }
			return Ok(language);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddLanguage([FromBody] LanguageModel languageModel)
		{
			languageModel.Id = await _languageRepository.AddAsync(languageModel);
			return CreatedAtAction(nameof(GetLanguageById), new { id = languageModel.Id }, languageModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateLanguage([FromRoute] int id, [FromBody] LanguageModel languageModel)
		{
			var result = await _languageRepository.UpdateAsync(id, languageModel);
			if (result != 1) { return BadRequest(); }
			return Ok(languageModel);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchDocument)
		{
			var result = await _languageRepository.UpdatePatchAsync(id, jsonPatchDocument);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _languageRepository.DeleteAsync(id);
			return Ok();
		}
	}
}
