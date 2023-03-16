using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		public readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public CategoryRepository(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CategoryModel>> GetAllAsync()
		{
			var records = await _context.Categories.ToListAsync();
			return _mapper.Map<List<CategoryModel>>(records);
		}

		public async Task<CategoryModel> GetByIdAsync(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			return _mapper.Map<CategoryModel>(category);
		}

		public async Task<int> AddAsync(CategoryModel model)
		{
			var category = _mapper.Map<Category>(model);
			await _context.Categories.AddAsync(category);
			_context.SaveChanges();
			return category.Id;
		}

		public async Task<int> UpdateAsync(int id, CategoryModel categoryModel)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category != null)
			{
				category.Name = categoryModel.Name;
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task<int> UpdatePatchAsync(int id, JsonPatchDocument categoryModel)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category != null)
			{
				categoryModel.ApplyTo(category);
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task DeleteAsync(int id)
		{
			var category = new Category() { Id = id };
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

		}
	}
}
