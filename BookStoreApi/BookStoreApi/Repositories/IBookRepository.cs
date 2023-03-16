using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Repositories
{
	public interface IBookRepository
	{
		Task<IEnumerable<BookModel>> GetAllAsync();
		Task<BookModel> GetByIdAsync(int id);
		Task<int> AddBookAsync(BookModel model);
		Task<int> UpdateBookAsync(int id, BookModel bookModel);
		Task<int> UpdateBookPatchAsync(int id, JsonPatchDocument bookModel);
		Task DeleteBookAsync(int id);
	}
}
