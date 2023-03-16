using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class LanguageRepository : ILanguageRepository
	{
		public readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public LanguageRepository(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<LanguageModel>> GetAllAsync()
		{
			var records = await _context.Languages.ToListAsync();
			return _mapper.Map<List<LanguageModel>>(records);
		}

		public async Task<LanguageModel> GetByIdAsync(int id)
		{
			var language = await _context.Languages.FindAsync(id);
			return _mapper.Map<LanguageModel>(language);
		}

		public async Task<int> AddAsync(LanguageModel model)
		{
			var language = _mapper.Map<Language>(model);
			await _context.Languages.AddAsync(language);
			_context.SaveChanges();
			return language.Id;
		}

		public async Task<int> UpdateAsync(int id, LanguageModel languageModel)
		{
			var language = await _context.Languages.FindAsync(id);
			if (language != null)
			{
				language.Name = languageModel.Name;
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task<int> UpdatePatchAsync(int id, JsonPatchDocument languageModel)
		{
			var language = await _context.Languages.FindAsync(id);
			if (language != null)
			{
				languageModel.ApplyTo(language);
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task DeleteAsync(int id)
		{
			var language = new Language() { Id = id };
			_context.Languages.Remove(language);
			await _context.SaveChangesAsync();

		}
	}
}
