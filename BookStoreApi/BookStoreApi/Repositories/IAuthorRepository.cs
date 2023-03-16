using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Repositories
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<AuthorModel>> GetAllAsync();
		Task<AuthorModel> GetByIdAsync(int id);
		Task<int> AddAsync(AuthorModel model);
		Task<int> UpdateAsync(int id, AuthorModel authorModel);
		Task<int> UpdatePatchAsync(int id, JsonPatchDocument authorModel);
		Task DeleteAsync(int id);
	}
}