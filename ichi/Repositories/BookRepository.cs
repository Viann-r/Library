using ichi.Models;

namespace ichi.Repositories
{
    internal class BookRepository
    {

        private readonly string _filePath = @"../../../Data/book.txt";

        public void SaveBook(Book book)
        {
            File.AppendAllLines(_filePath, [book.ToString()]);
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            var lines = File.ReadAllLines(_filePath);
            foreach (var item in lines)
            {
                var line = item.Split("|");
                Book book = new Book();
                book.Id = int.Parse(line[0]);
                book.Name = line[1];
                book.Description = line[2];
                books.Add(book);
            }
            return books;
        }


        public Book GetBookById(int id)
        {
            Book book = new Book();
            var books = GetAllBooks();
            book = books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        public void DeleteBook(int id)
        {
            var books = GetAllBooks();
            Book book = new Book();
            book = books.FirstOrDefault(x => x.Id == id);
            books.Remove(book);
            string[] bookText = new string[books.Count];
            for (int i = 0; i < books.Count; i++)
            {
                bookText[i] = books[i].ToString();
            }
            File.WriteAllText(_filePath, string.Empty);
            File.AppendAllLines(_filePath, bookText);
        }

        public void UpdateBook(Book book)
        {
            var books = GetAllBooks();
            Book bookToUpdate = new Book();
            bookToUpdate = books.FirstOrDefault(x => x.Id == book.Id);
            bookToUpdate.Name = book.Name;
            bookToUpdate.Description = book.Description;
            string[] bookText = new string[books.Count];
            for (int i = 0; i < books.Count; i++)
            {
                bookText[i] = books[i].ToString();
            }
            File.WriteAllText(_filePath, string.Empty);
            File.AppendAllLines(_filePath, bookText);
        }





    }


}
