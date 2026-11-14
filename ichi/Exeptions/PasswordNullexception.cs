namespace ichi.Exeptions
{
    internal class PasswordNullexception : Exception
    {
        public PasswordNullexception() : base("Password cannot be null.")
        {
        }

        public PasswordNullexception(string? message) : base(message)
        {
        }
    }
}
