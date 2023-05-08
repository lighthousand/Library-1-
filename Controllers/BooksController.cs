using Library.Data;
using Library.Factories;
using Library.Factories.Interfaces;
using Library.Models;
using Library.Models.Interfaces;
using Library.ViewModels;
using Library.ViewModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly IModelsAbstractFactory<IAddBookViewModel> _addBookViewModelsFactory;
        private readonly IModelsAbstractFactory<IUpdateBookViewModel> _updateBookViewModelsFactory;
        private readonly IModelsAbstractFactory<IBook> _modelsFactory;

        public BooksController(LibraryDbContext dbContext, 
            IModelsAbstractFactory<IAddBookViewModel> addBookViewModelsFactory, 
            IModelsAbstractFactory<IUpdateBookViewModel> updateBookViewModelsFactory, 
            IModelsAbstractFactory<IBook> modelsFactory)
        {
            _context = dbContext;
            _addBookViewModelsFactory = addBookViewModelsFactory;
            _updateBookViewModelsFactory = updateBookViewModelsFactory;
            _modelsFactory = modelsFactory;
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
            var book = _modelsFactory.Create();
            book.Id = Guid.NewGuid();
            book.Title = bookData.Title;
            book.Description = bookData.Description;
            book.Authors = bookData.Authors;
            book.ImageCover = bookData.ImageCover;

            _context.Books.Add((Book)book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            var book = _context.Books.FirstOrDefault(_context => _context.Id == id);

            if (book != null)
            {
                var viewModel = _updateBookViewModelsFactory.Create();
                viewModel.Title = book.Title;
                viewModel.Description = book.Description;
                viewModel.Authors = string.Join(",", book.Authors);
                viewModel.ImageCover = book.ImageCover;             

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