namespace ichi.Exeptions
{
    internal class VerificationCodeNullException : Exception
    {
        public VerificationCodeNullException() : base("Verification code cannot be null.")
        {
        }
        public VerificationCodeNullException(string? message) : base(message)
        {
        }
    }
}
