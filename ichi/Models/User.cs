using ichi.Exeptions;

namespace ichi.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (!value.Contains("@"))
                {
                    //Console.WriteLine("email invalid");
                    //throw new Exception("Email invalid");
                    throw new InvalidEmailExeption("Email invalid");

                }
                _email = value;
            }
        }

        public string Password { get; set; }
        public bool IsLogged { get; set; } = false;
        public string VerificationCode { get; set; }
        public bool IsVerified { get; set; } = false;

        public override string? ToString()
        {
            return $"{Id}|{Name}|{Email}|{Password}|{IsLogged}|{VerificationCode}|{IsVerified}";
        }
    }
}
