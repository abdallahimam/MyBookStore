using BookStoreApi.Models;

namespace BookStoreApi.Repositories
{
	public interface IEBookRepository
	{
		Task<int> AddEBookAsync(EBookModel eBookModel);
		Task<int> DeleteEBookAsync(int id);
		Task<IEnumerable<EBookModel>> GetAllEBooksAsync();
		Task<EBookModel> GetEBookByIdAsync(int id);
		Task<IEnumerable<EBookModel>> GetEBooksByBookIdAsync(int bookId);
		Task<int> UpdateEBookAsync(int id, EBookModel eBookModel);
	}
}