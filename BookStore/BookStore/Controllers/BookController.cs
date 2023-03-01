using BookStore.Models;
using BookStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: BookController
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooks();
            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            ViewBag.SimilarBooks = await _bookRepository.GetSimilarBooks(20);
            return View(book);
        }

        // GET: BookController/Create
        public async Task<ActionResult> Create(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.bookId = bookId;

            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Language");
            //ViewBag.Language = GetLanguageListItem();
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.CategoryM = GetLanguageListItem();
            var model = new BookModel() { LanguageId = 1, CategoryM = new List<string>() { "1", "2" }, LanguageEnum = Enums.LanguageEnum.Hindi};

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> Create(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.Cover != null)
                {
                    string localFolder = "books/cover/" + Guid.NewGuid().ToString() + "_" + bookModel.Cover.FileName;
                    string coverPath = await UploadImage(localFolder, bookModel.Cover);
                    bookModel.CoverPath = coverPath;
                }
                if (bookModel.GalleryFiles != null)
                {
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        string localFolder = "books/gallery/" + Guid.NewGuid().ToString() + "_" + file.FileName;
                        string coverPath = await UploadImage(localFolder, file);
                        bookModel.Gallery.Add(new GalleryModel() 
                        {
                            Name = file.FileName,
                            Path = coverPath
                        });
                    }
                }
                if (bookModel.BookPdf != null)
                {
                    string localFolder = "books/pdf/" + Guid.NewGuid().ToString() + "_" + bookModel.BookPdf.FileName;
                    string coverPath = await UploadImage(localFolder, bookModel.BookPdf);
                    bookModel.BookPdfPath = coverPath;
                }
                bookModel.Id = await _bookRepository.AddBook(bookModel);
                if (bookModel.Id > 0)
                {
                    return RedirectToAction(nameof(Create), new { isSuccess = true, bookId = bookModel.Id });
                }
            }
            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Language");
            //ViewBag.Language = GetLanguageListItem();
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.CategoryM = GetLanguageListItem();
            return View();
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
        private List<SelectListItem> GetLanguageListItem()
        {
            return new List<SelectListItem>(){
                new SelectListItem() { Text = "Arabic", Value = 1.ToString() },
                new SelectListItem() { Text = "English", Value = 2.ToString() },
                new SelectListItem() { Text = "Ducth", Value = 3.ToString() },
                new SelectListItem() { Text = "French", Value = 4.ToString() }};
        }
        
        private async Task<string> UploadImage(string localFolder, IFormFile file)
        {
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, localFolder);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + localFolder;
        }
    }
}
