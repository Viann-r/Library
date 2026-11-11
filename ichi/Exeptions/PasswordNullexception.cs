namespace ichi.Exeptions
{
    internal class PasswordNullexception : Exception
    {
        public PasswordNullexception() : base("Email cannot be null.")
        {
        }

        public PasswordNullexception(string? message) : base(message)
        {
        }
    }
}
