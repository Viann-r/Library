namespace ichi.Models
{
    internal class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Vol { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Instock { get; set; }
        public override string? ToString()
        {
            return $"{Id}|{Name}|{Vol}|{Description}|{Price}|{Instock}";
        }
    }
}
