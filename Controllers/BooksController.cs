using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;
        public BooksController(LibraryDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _context.Books.ToList();

            return View(books);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel bookData)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = bookData.Title,
                Description = bookData.Description,
                Authors = bookData.Authors.ToArray(),
                //ImageCover = bookData.ImageCover,
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            var book = _context.Books.FirstOrDefault(_context => _context.Id == id);

            if (book != null)
            {
                var viewModel = new UpdateBookViewModel
                {
                    Title = book.Title,
                    Description = book.Description,
                    Authors = string.Join(',', book.Authors.ToArray()),
                    //ImageCover = bookData.ImageCover,
                };
                
                return View("View", viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(UpdateBookViewModel model)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == model.Id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Description = model.Description;
                book.Authors = model.Authors.Split(",");

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(UpdateBookViewModel model)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == model.Id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
