using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Repositories
{
	public interface ISectionRepository
	{
		Task<int> AddAsync(SectionModel model);
		Task DeleteAsync(int id);
		Task<IEnumerable<SectionModel>> GetAllAsync();
		Task<SectionModel> GetByIdAsync(int id);
		Task<int> UpdateAsync(int id, SectionModel sectionModel);
		Task<int> UpdatePatchAsync(int id, JsonPatchDocument sectionModel);
	}
}