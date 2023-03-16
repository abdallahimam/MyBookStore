using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		public readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public AuthorRepository(BookStoreDbContext context, IMapper mapper) {
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<AuthorModel>> GetAllAsync()
		{
			var records = await _context.Authors.ToListAsync();
			return _mapper.Map<List<AuthorModel>>(records);
		}

		public async Task<AuthorModel> GetByIdAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			return _mapper.Map<AuthorModel>(author);
		}

		public async Task<int> AddAsync(AuthorModel model)
		{
			var author = _mapper.Map<Author>(model);
			await _context.Authors.AddAsync(author);
			_context.SaveChanges();
			return author.Id;
		}

		public async Task<int> UpdateAsync(int id, AuthorModel authorModel)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author != null)
			{
				author.Name = authorModel.Name;
				author.Gender = authorModel.Gender;
				author.BirthDate = authorModel.BirthDate;
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task<int> UpdatePatchAsync(int id, JsonPatchDocument authorModel)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author != null)
			{
				authorModel.ApplyTo(author);
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task DeleteAsync(int id)
		{
			var author = new Author() { Id = id };
			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();
			
		}
	}
}
