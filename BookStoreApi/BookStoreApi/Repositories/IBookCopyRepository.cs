using BookStoreApi.Models;

namespace BookStoreApi.Repositories
{
	public interface IBookCopyRepository
	{
		Task<int> AddBookCopyAsync(BookCopyModel bookCopyModel);
		Task<int> DeleteBookCopyAsync(int id);
		Task<IEnumerable<BookCopyModel>> GetAllBookCopiesAsync();
		Task<IEnumerable<BookCopyModel>> GetBookCopiesByBookIdAsync(int bookId);
		Task<BookCopyModel> GetBookCopyByIdAsync(int id);
		Task<int> UpdateBookCopyStatusAsync(int id, string Status);
		Task<int> UpdateBookCopyAsync(int id, BookCopyModel bookCopyModel);
	}
}