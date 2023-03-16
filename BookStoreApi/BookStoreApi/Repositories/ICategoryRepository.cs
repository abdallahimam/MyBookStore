using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Repositories
{
	public interface ICategoryRepository
	{
		Task<int> AddAsync(CategoryModel model);
		Task DeleteAsync(int id);
		Task<IEnumerable<CategoryModel>> GetAllAsync();
		Task<CategoryModel> GetByIdAsync(int id);
		Task<int> UpdateAsync(int id, CategoryModel categoryModel);
		Task<int> UpdatePatchAsync(int id, JsonPatchDocument categoryModel);
	}
}