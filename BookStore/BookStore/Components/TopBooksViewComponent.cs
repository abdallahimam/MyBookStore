using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository = null;

        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var topBooks = await _bookRepository.GetSimilarBooks(count);
            return View(topBooks);
        }
    }
}
