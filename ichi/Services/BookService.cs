using ichi.Models;
using ichi.Repositories;

namespace ichi.Services
{
    internal class BookService
    {
        private readonly BookRepository _bookRepository;
        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }
    }
}
