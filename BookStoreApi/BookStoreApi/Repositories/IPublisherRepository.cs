using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Repositories
{
	public interface IPublisherRepository
	{
		Task<int> AddAsync(PublisherModel model);
		Task DeleteAsync(int id);
		Task<IEnumerable<PublisherModel>> GetAllAsync();
		Task<PublisherModel> GetByIdAsync(int id);
		Task<int> UpdateAsync(int id, PublisherModel publisherModel);
		Task<int> UpdatePatchAsync(int id, JsonPatchDocument publisherModel);
	}
}