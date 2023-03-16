using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Helpers;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class BookCopyRepository : IBookCopyRepository
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public BookCopyRepository(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BookCopyModel>> GetAllBookCopiesAsync()
		{
			var records = await _context.BookCopies.ToListAsync();
			return _mapper.Map<List<BookCopyModel>>(records);
		}

		public async Task<BookCopyModel> GetBookCopyByIdAsync(int id)
		{
			var bookCopy = await _context.BookCopies.FindAsync(id);
			return _mapper.Map<BookCopyModel>(bookCopy);
		}

		public async Task<IEnumerable<BookCopyModel>> GetBookCopiesByBookIdAsync(int bookId)
		{
			var records = await _context.BookCopies.Select(bc => bc.BookId == bookId).ToListAsync();
			return _mapper.Map<List<BookCopyModel>>(records);
		}

		public async Task<int> AddBookCopyAsync(BookCopyModel bookCopyModel)
		{
			var bookCopy = _mapper.Map<BookCopy>(bookCopyModel);
			await _context.BookCopies.AddAsync(bookCopy);
			await _context.SaveChangesAsync();
			return bookCopy.Id;
		}
		public async Task<int> UpdateBookCopyAsync(int id, BookCopyModel bookCopyModel)
		{
			var bookCopy = await _context.BookCopies.FindAsync(id);
			if (bookCopy == null) { return 0; }

			bookCopy.Status = bookCopyModel.Status;
			bookCopy.BookId = bookCopyModel.Id;
			await _context.SaveChangesAsync();
			return 1;
		}

		public async Task<int> UpdateBookCopyStatusAsync(int id, String Status)
		{
			var bookCopy = await _context.BookCopies.FindAsync(id);
			if (bookCopy == null) { return 0; }

			bookCopy.Status = Status;
			await _context.SaveChangesAsync();
			return 1;
		}

		public async Task<int> DeleteBookCopyAsync(int id)
		{
			var bookCopy = new BookCopy() { Id = id };
			if (bookCopy == null) { return 0; }
			_context.BookCopies.Remove(bookCopy);
			await _context.SaveChangesAsync();
			return 1;
		}
	}
}
