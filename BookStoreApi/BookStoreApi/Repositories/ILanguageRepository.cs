using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Repositories
{
	public interface ILanguageRepository
	{
		Task<int> AddAsync(LanguageModel model);
		Task DeleteAsync(int id);
		Task<IEnumerable<LanguageModel>> GetAllAsync();
		Task<LanguageModel> GetByIdAsync(int id);
		Task<int> UpdateAsync(int id, LanguageModel languageModel);
		Task<int> UpdatePatchAsync(int id, JsonPatchDocument languageModel);
	}
}