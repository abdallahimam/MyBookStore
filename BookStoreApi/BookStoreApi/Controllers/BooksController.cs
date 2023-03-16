using BookStoreApi.Data;
using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		public readonly IBookRepository _bookRepository;

		public BooksController(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetBooks()
		{
			var books = await _bookRepository.GetAllAsync();
			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookById([FromRoute]int id)
		{
			var book = await _bookRepository.GetByIdAsync(id);
			if (book == null) { return NotFound(); }
			return Ok(book);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddBook([FromBody]BookModel book)
		{
			book.Id = await _bookRepository.AddBookAsync(book);
			return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel book)
		{
			int result = await _bookRepository.UpdateBookAsync(id, book);
			if (result != 1)
			{ return NotFound(); }
			return Ok();
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument book)
		{
			int result = await _bookRepository.UpdateBookPatchAsync(id, book);
			if (result != 1)
			{ return NotFound(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
		{
			await _bookRepository.DeleteBookAsync(id);
			return Ok();
		}
	}
}
