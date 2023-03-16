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
		Task<IEnumerable<BookModel>> GetBooksByAuthorAsync(int authorId);
		Task<IEnumerable<BookModel>> GetBooksByPublisherAsync(int publisherId);
		Task<IEnumerable<BookModel>> GetBooksByLanguageAsync(int languageId);
		Task<IEnumerable<BookModel>> GetBooksBySectionAsync(int sectionId);
		Task<IEnumerable<BookModel>> GetBooksByCategoryAsync(int categoryId);
	}
}
