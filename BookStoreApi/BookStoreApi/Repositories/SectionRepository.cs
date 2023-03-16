using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories
{
	public class SectionRepository : ISectionRepository
	{
		public readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public SectionRepository(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<SectionModel>> GetAllAsync()
		{
			var records = await _context.Sections.ToListAsync();
			return _mapper.Map<List<SectionModel>>(records);
		}

		public async Task<SectionModel> GetByIdAsync(int id)
		{
			var section = await _context.Sections.FindAsync(id);
			return _mapper.Map<SectionModel>(section);
		}

		public async Task<int> AddAsync(SectionModel model)
		{
			var section = _mapper.Map<Section>(model);
			await _context.Sections.AddAsync(section);
			_context.SaveChanges();
			return section.Id;
		}

		public async Task<int> UpdateAsync(int id, SectionModel sectionModel)
		{
			var section = await _context.Sections.FindAsync(id);
			if (section != null)
			{
				section.Name = sectionModel.Name;
				section.CategoryId = sectionModel.CategoryId;
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task<int> UpdatePatchAsync(int id, JsonPatchDocument sectionModel)
		{
			var section = await _context.Sections.FindAsync(id);
			if (section != null)
			{
				sectionModel.ApplyTo(section);
				await _context.SaveChangesAsync();
				return 1;
			}
			return 0;
		}

		public async Task DeleteAsync(int id)
		{
			var section = new Section() { Id = id };
			_context.Sections.Remove(section);
			await _context.SaveChangesAsync();

		}
	}
}
