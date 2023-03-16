using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class PublisherRepository : IPublisherRepository
	{
		public readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public PublisherRepository(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<PublisherModel>> GetAllAsync()
		{
			var records = await _context.Publishers.ToListAsync();
			return _mapper.Map<List<PublisherModel>>(records);
		}

		public async Task<PublisherModel> GetByIdAsync(int id)
		{
			var publisher = await _context.Publishers.FindAsync(id);
			return _mapper.Map<PublisherModel>(publisher);
		}

		public async Task<int> AddAsync(PublisherModel model)
		{
			var publisher = _mapper.Map<Publisher>(model);
			await _context.Publishers.AddAsync(publisher);
			_context.SaveChanges();
			return publisher.Id;
		}

		public async Task<int> UpdateAsync(int id, PublisherModel publisherModel)
		{
			var publisher = await _context.Publishers.FindAsync(id);
			if (publisher != null)
			{
				publisher.Name = publisherModel.Name;
				publisher.Location = publisherModel?.Location;
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task<int> UpdatePatchAsync(int id, JsonPatchDocument publisherModel)
		{
			var publisher = await _context.Publishers.FindAsync(id);
			if (publisher != null)
			{
				publisherModel.ApplyTo(publisher);
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task DeleteAsync(int id)
		{
			var publisher = new Publisher() { Id = id };
			_context.Publishers.Remove(publisher);
			await _context.SaveChangesAsync();

		}
	}
}
