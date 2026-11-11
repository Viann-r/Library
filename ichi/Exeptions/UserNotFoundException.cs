namespace ichi.Exeptions
{
    internal class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.")
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
