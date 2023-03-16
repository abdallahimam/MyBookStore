using BookStoreApi.Models;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublishersController : ControllerBase
	{

		private readonly IPublisherRepository _publisherRepository;

		public PublishersController(IPublisherRepository publisherRepository)
		{
			_publisherRepository = publisherRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetPublishers()
		{
			var records = await _publisherRepository.GetAllAsync();
			return Ok(records);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPublisherById([FromRoute] int id) 
		{ 
			var publisher = await _publisherRepository.GetByIdAsync(id);
			if (publisher == null) { return NotFound(); }
			return Ok(publisher);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddPublisher([FromBody] PublisherModel publisherModel)
		{
			publisherModel.Id = await _publisherRepository.AddAsync(publisherModel);
			return CreatedAtAction(nameof(GetPublisherById), new { id = publisherModel.Id }, publisherModel);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePublisher([FromRoute] int id, [FromBody] PublisherModel publisherModel)
		{
			var result = await _publisherRepository.UpdateAsync(id, publisherModel);
			if (result != 1) { return BadRequest(); }
			return Ok(publisherModel);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchDocument)
		{
			var result = await _publisherRepository.UpdatePatchAsync(id, jsonPatchDocument);
			if (result != 1) { return BadRequest(); }
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _publisherRepository.DeleteAsync(id);
			return Ok();
		}
	}
}
