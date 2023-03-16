using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{

		private readonly IAuthorRepository _authorRepository;

		public AuthorsController(IAuthorRepository authorRepository)
		{
			_authorRepository = authorRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAuthors()
		{
			var records = await _authorRepository.GetAllAsync();
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAuthorById([FromRoute] int id) 
		{ 
			var author = await _authorRepository.GetByIdAsync(id);
			if (author == null) { return NotFound(); }
			return Ok(author);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddAuthor([FromBody] AuthorModel authorModel)
		{
			authorModel.Id = await _authorRepository.AddAsync(authorModel);
			return CreatedAtAction(nameof(GetAuthorById), new { id = authorModel.Id }, authorModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorModel authorModel)
		{
			var result = await _authorRepository.UpdateAsync(id, authorModel);
			if (result != 1) { return BadRequest(); }
			return Ok(authorModel);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchDocument)
		{
			var result = await _authorRepository.UpdatePatchAsync(id, jsonPatchDocument);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _authorRepository.DeleteAsync(id);
			return Ok();
		}
	}
}
