namespace ichi.Exeptions
{
    internal class InvalidEmailExeption : Exception
    {
        public InvalidEmailExeption()
        {

        }

        public InvalidEmailExeption(string? message) : base(message)
        {

        }
    }
}
