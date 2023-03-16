using BookStoreApi.Helpers;
using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EBooksController : ControllerBase
	{

		private readonly IEBookRepository _eBookRepository;

		public EBooksController(IEBookRepository eBookRepository)
		{
			_eBookRepository= eBookRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetEBooks()
		{
			var records = await _eBookRepository.GetAllEBooksAsync();
			return Ok(records);
		}

		[HttpGet("~/api/books/{bookid}/ebooks")]
		public async Task<IActionResult> GetEBooksByBookId([FromRoute] int bookid) 
		{ 
			var records = await _eBookRepository.GetEBooksByBookIdAsync(bookid);
			if (records == null) { return NotFound(); }
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetEBookById([FromRoute] int id)
		{
			var record = await _eBookRepository.GetEBookByIdAsync(id);
			if (record == null) { return NotFound(); }
			return Ok(record);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddEBook([FromBody] EBookModel eBookModel)
		{
			eBookModel.Id = await _eBookRepository.AddEBookAsync(eBookModel);
			return CreatedAtAction(nameof(GetEBookById), new { id = eBookModel.Id }, eBookModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateEBookStatus([FromRoute] int id, [FromBody] EBookModel eBookModel)
		{
			var result = await _eBookRepository.UpdateEBookAsync(id, eBookModel);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			int result = await _eBookRepository.DeleteEBookAsync(id);
			if (result != 1)
			{ return BadRequest(); }
			return Ok();
		}
	}
}
