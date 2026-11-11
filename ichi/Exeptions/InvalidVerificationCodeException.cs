namespace ichi.Exeptions
{
    internal class InvalidVerificationCodeException : Exception
    {
        public InvalidVerificationCodeException() : base("Invalid verification code.")
        {
        }
        public InvalidVerificationCodeException(string? message) : base(message)
        {
        }
    }
}
