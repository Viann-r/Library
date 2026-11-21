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
        public Book? GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public void AddBook(string name, int vol, string description)
        {
            CheckBookData(name, vol, description);

            List<Book> books = GetAllBooks();
            Book book = new Book();
            book.Id = GetNextId(books);
            book.Name = name.Trim();
            book.Vol = vol;
            book.Description = description.Trim();

            _bookRepository.SaveBook(book);
        }
        public bool UpdateBook(int id, string name, int vol, string description)
        {
            CheckBookData(name, vol, description);

            Book? book = GetBookById(id);
            if (book == null)
                return false;

            book.Name = name.Trim();
            book.Vol = vol;
            book.Description = description.Trim();
            _bookRepository.UpdateBook(book);
            return true;
        }

        public bool DeleteBook(int id)
        {
            Book? book = GetBookById(id);
            if (book == null)
                return false;

            _bookRepository.DeleteBook(id);
            return true;
        }
        public List<Book> SearchBooks(string text)
        {
            List<Book> result = new List<Book>();
            List<Book> books = GetAllBooks();
            if (string.IsNullOrWhiteSpace(text))
                return books;

            foreach (Book book in books)
            {
                if (book.Name.Contains(text, StringComparison.OrdinalIgnoreCase)
                    || book.Description.Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(book);
                }
            }

            return result;
        }

        private static int GetNextId(List<Book> books)
        {
            int biggestId = 0;
            foreach (Book book in books)
            {
                if (book.Id > biggestId)
                    biggestId = book.Id;
            }
            return biggestId + 1;
        }
        private static void CheckBookData(string name, int vol, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Book name cannot be empty.");
            if (vol <= 0)
                throw new ArgumentException("Book volume must be a positive number.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Book description cannot be empty.");
        }

    }
}
