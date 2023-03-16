using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class BookRepository : IBookRepository
	{
		public readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public BookRepository(BookStoreDbContext context, IMapper mapper) {
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BookModel>> GetAllAsync()
		{
			var records = await _context.Books
				.Include(a => a.Author)
				.Include(p => p.Publisher)
				.Include(l => l.Language)
				.Include(s => s.Section).ThenInclude(c => c.Category)
				.Include(eb => eb.EBooks)
				.Include(bc => bc.BookCopies)
				.ToListAsync();
			return _mapper.Map<List<BookModel>>(records);
		}

		
		public async Task<BookModel> GetByIdAsync(int id)
		{
			var book = await _context.Books
				.Include(a => a.Author)
				.Include(p => p.Publisher)
				.Include(l => l.Language)
				.Include(s => s.Section).ThenInclude(c => c.Category)
				.Include(eb => eb.EBooks)
				.Include(bc => bc.BookCopies)
				.FirstOrDefaultAsync(i => i.Id == id);
			return _mapper.Map<BookModel>(book);
		}

		public async Task<int> AddBookAsync(BookModel model)
		{
			var book = _mapper.Map<Book>(model);
			await _context.Books.AddAsync(book);
			_context.SaveChanges();
			return book.Id;
		}

		public async Task<int> UpdateBookAsync(int id, BookModel bookModel)
		{
			var book = await _context.Books.FindAsync(id);
			if (book != null)
			{
				book.Title = bookModel.Title;
				book.Description = bookModel.Description;
				book.ISBNNumber = bookModel.ISBNNumber;
				book.Pages = bookModel.Pages;
				book.Edition = bookModel.Edition;
				book.AuthorId = bookModel.AuthorId;
				book.PublisherId = bookModel.PublisherId;
				book.LanguageId = bookModel.LanguageId;
				book.SectionId = bookModel.SectionId;

				await _context.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<int> UpdateBookPatchAsync(int id, JsonPatchDocument bookModel)
		{
			var book = await _context.Books.FindAsync(id);
			if (book!= null)
			{
				bookModel.ApplyTo(book);
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task DeleteBookAsync(int id)
		{
			var book = new Book() { Id = id };
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
			
		}
	}
}
