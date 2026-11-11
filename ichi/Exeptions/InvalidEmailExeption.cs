namespace ichi.Exeptions
{
    internal class InvalidEmailExeption : Exception
    {
        public InvalidEmailExeption() : base("invalid email address.")
        {

        }

        public InvalidEmailExeption(string? message) : base(message)
        {

        }
    }
}
