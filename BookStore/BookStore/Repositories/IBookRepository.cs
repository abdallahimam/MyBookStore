using BookStore.Models;

namespace BookStore.Repositories
{
    public interface IBookRepository
    {
        Task<int> AddBook(BookModel bookModel);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel?> GetBookById(int id);
        Task<List<BookModel>> GetSimilarBooks(int count = 0);
    }
}