using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Helpers;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class EBookRepository : IEBookRepository
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public EBookRepository(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<EBookModel>> GetAllEBooksAsync()
		{
			var records = await _context.EBooks.ToListAsync();
			return _mapper.Map<List<EBookModel>>(records);
		}

		public async Task<EBookModel> GetEBookByIdAsync(int id)
		{
			var eBook = await _context.EBooks.FindAsync(id);
			return _mapper.Map<EBookModel>(eBook);
		}

		public async Task<IEnumerable<EBookModel>> GetEBooksByBookIdAsync(int bookId)
		{
			var records = await _context.EBooks.Select(eb => eb.BookId == bookId).ToListAsync();
			return _mapper.Map<List<EBookModel>>(records);
		}

		public async Task<int> AddEBookAsync(EBookModel eBookModel)
		{
			var eBook = _mapper.Map<EBook>(eBookModel);
			await _context.EBooks.AddAsync(eBook);
			await _context.SaveChangesAsync();
			return eBook.BookId;
		}

		public async Task<int> UpdateEBookAsync(int id, EBookModel eBookModel)
		{
			var eBook = await _context.EBooks.FindAsync(id);
			if (eBook == null) { return 0; }
			eBook.BookId = eBookModel.BookId;
			eBook.Extension = eBookModel.Extension;
			await _context.SaveChangesAsync();
			return 1;
		}

		public async Task<int> DeleteEBookAsync(int id)
		{
			var eBook = new EBook() { Id = id };
			if (eBook == null) { return 0; }
			_context.EBooks.Remove(eBook);
			await _context.SaveChangesAsync();
			return 1;
		}
	}
}
