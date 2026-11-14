namespace ichi.Exeptions
{
    internal class UsedEmailException : Exception
    {
        public UsedEmailException() : base("Email is already registered")
        {
        }

        public UsedEmailException(string? message) : base(message)
        {
        }
    }
}
