namespace ichi.Exeptions
{
    internal class EmailNullexception : Exception
    {
        public EmailNullexception() : base("Email cannot be null.")
        {
        }

        public EmailNullexception(string? message) : base(message)
        {
        }
    }
}
