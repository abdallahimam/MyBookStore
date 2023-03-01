using BookStore.Controllers;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreDbContext _dbContext = null;

        public LanguageRepository(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public async Task<LanguageModel?> GetLanguageById(int id)
        {
            return await _dbContext.Languages.Where(x => x.Id == id)
                 .Select(language => new LanguageModel()
                 {
                     Name = language.Name,
                     Description = language.Description,
                 }).FirstOrDefaultAsync();
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            var languages = new List<LanguageModel>();
            var dbLanguages = await _dbContext.Languages.ToListAsync();
            if (dbLanguages?.Any() == true)
            {
                foreach (var language in dbLanguages)
                {
                    languages.Add(new LanguageModel()
                    {
                        Id = language.Id,
                        Name = language.Name,
                        Description = language.Description,
                    });
                }
            }
            return languages;
        }
    }
}
