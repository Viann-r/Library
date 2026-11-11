namespace ichi.Exeptions
{
    internal class PasswordNotMatchException : Exception
    {
        public PasswordNotMatchException() : base("Password does not match")
        {
        }

        public PasswordNotMatchException(string? message) : base(message)
        {
        }
    }
}
