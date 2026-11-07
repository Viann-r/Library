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

        //public void DeleteBook(int Id)
        //{
        //    var books = GetAllBooks();
        //    Book book = new Book();
        //    book = book.FirstOrDefault();
        //    var bookToDelete = books.FirstOrDefault(b => b.Id == Id);
        //    if (bookToDelete != null)
        //    {
        //        books.Remove(bookToDelete);
        //        File.WriteAllLines(_filePath, books.Select(b => b.ToString()));
        //    }







    }


}
