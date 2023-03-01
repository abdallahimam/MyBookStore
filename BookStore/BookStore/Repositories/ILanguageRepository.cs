using BookStore.Models;

namespace BookStore.Repositories
{
    public interface ILanguageRepository
    {
        Task<LanguageModel?> GetLanguageById(int id);
        Task<List<LanguageModel>> GetLanguages();
    }
}