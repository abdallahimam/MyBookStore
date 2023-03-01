using BookStore.Controllers;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _dbContext = null;

        public BookRepository(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public async Task<int> AddBook(BookModel bookModel)
        {
            var book = new Book()
            {
                Title = bookModel.Title,
                Author = bookModel.Author,
                Description = bookModel.Description,
                TotalPages = bookModel.TotalPages.HasValue ? bookModel.TotalPages.Value : 0,
                Category = bookModel.Category,
                LanguageId = bookModel.LanguageId,
                CreatedOn = bookModel.CreatedOn,
                UpdatedOn = bookModel.UpdatedOn,
                CoverPath = bookModel.CoverPath,
                BookPdfPath = bookModel.BookPdfPath,
            };
            book.BookGallery = new List<BookGallery>();
            foreach (var gallery in bookModel.Gallery)
            {
                book.BookGallery.Add(new BookGallery()
                {
                    Name = gallery.Name,
                    Path = gallery.Path
                });
            }
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book.Id;
        }

        public async Task<BookModel?> GetBookById(int id)
        {
            return await _dbContext.Books.Where(x => x.Id == id)
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     LanguageId = book.LanguageId,
                     Language = new LanguageModel() { Id = book.LanguageId, Name = book.Language.Name, Description = book.Language.Description },
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverPath = book.CoverPath,
                     BookPdfPath = book.BookPdfPath,
                     Gallery = book.BookGallery != null ? book.BookGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         Path = g.Path
                     }).ToList() : new List<GalleryModel>()
                 }).FirstOrDefaultAsync();
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var dbBooks = await _dbContext.Books.ToListAsync();
            if (dbBooks?.Any() == true)
            {
                foreach (var book in dbBooks)
                {
                    books.Add(new BookModel()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        TotalPages = book.TotalPages,
                        Category = book.Category,
                        LanguageId = book.LanguageId,
                        CreatedOn = book.CreatedOn,
                        UpdatedOn = book.UpdatedOn,
                        CoverPath = book.CoverPath
                    });
                }
            }
            return books;
        }
        public async Task<List<BookModel>> GetSimilarBooks(int count = 0)
        {
            var books = new List<BookModel>();
            var dbBooks = new List<Book>();
            if (count == 0)
                dbBooks = await _dbContext.Books.ToListAsync();
            else
                dbBooks = await _dbContext.Books.Take<Book>(count).ToListAsync();
            if (dbBooks?.Any() == true)
            {

                foreach (var book in dbBooks)
                {
                    books.Add(new BookModel()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        TotalPages = book.TotalPages,
                        Category = book.Category,
                        LanguageId = book.LanguageId,
                        CreatedOn = book.CreatedOn,
                        UpdatedOn = book.UpdatedOn,
                        CoverPath = book.CoverPath
                    });
                }
            }
            return books;
        }
    }
}
