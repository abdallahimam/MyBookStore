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
	public class BookCopiesController : ControllerBase
	{

		private readonly IBookCopyRepository _bookCopyRepository;

		public BookCopiesController(IBookCopyRepository bookCopyRepository)
		{
			_bookCopyRepository= bookCopyRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetBookCopies()
		{
			var records = await _bookCopyRepository.GetAllBookCopiesAsync();
			return Ok(records);
		}

		[HttpGet("~/api/books/{bookid}/bookcopies")]
		public async Task<IActionResult> GetBookCopiesByBookId([FromRoute] int bookid) 
		{ 
			var records = await _bookCopyRepository.GetBookCopiesByBookIdAsync(bookid);
			if (records == null) { return NotFound(); }
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookCopyById([FromRoute] int id)
		{
			var record = await _bookCopyRepository.GetBookCopyByIdAsync(id);
			if (record == null) { return NotFound(); }
			return Ok(record);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddBookCopy([FromBody] BookCopyModel bookCopyModel)
		{
			bookCopyModel.Id = await _bookCopyRepository.AddBookCopyAsync(bookCopyModel);
			return CreatedAtAction(nameof(GetBookCopyById), new { id = bookCopyModel.Id }, bookCopyModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBookCopy([FromRoute] int id, [FromBody] BookCopyModel bookCopyModel)
		{
			if (!BookCopyStatus.CheckBookCopyStatus(bookCopyModel.Status)) { return BadRequest(); }
			var result = await _bookCopyRepository.UpdateBookCopyAsync(id, bookCopyModel);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBookCopyStatus([FromRoute] int id, [FromBody] string status)
		{
			if (!BookCopyStatus.CheckBookCopyStatus(status)) { return BadRequest(); }
			var result = await _bookCopyRepository.UpdateBookCopyStatusAsync(id, status);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			int result = await _bookCopyRepository.DeleteBookCopyAsync(id);
			if (result != 1)
			{ return BadRequest(); }
			return Ok();
		}
	}
}
