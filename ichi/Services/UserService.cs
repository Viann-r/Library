using ichi.Exeptions;
using ichi.Models;
using ichi.Repositories;

namespace ichi.Services
{
    internal class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly EmailService _emailService;
        public UserService(UserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public void RegisterUser(User user)
        {
            var users = _userRepository.GetAllUsers();
            int maxId = 0;
            if (users.Count != 0)
            {
                maxId = users.Max(u => u.Id);

            }
            user.Id = ++maxId;
            if (user == null)
            {
                Console.WriteLine("user cannot be null");
                return;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            Random random = new Random();
            var randomCode = random.Next(1000, 9999);
            user.VerificationCode = randomCode.ToString();
            _emailService.SendEmail(user.Email, "Welcome to MangaLibrary", $"string welcomeHtmlMessage = @\"\r\n<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"\"utf-8\"\">\r\n    <style>\r\n        body {{\r\n            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\r\n            background-color: #f4f4f7;\r\n            margin: 0;\r\n            padding: 0;\r\n            color: #51545e;\r\n        }}\r\n        .email-wrapper {{\r\n            width: 100%;\r\n            background-color: #f4f4f7;\r\n            padding: 40px 0;\r\n        }}\r\n        .email-content {{\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            background-color: #ffffff;\r\n            border-radius: 8px;\r\n            overflow: hidden;\r\n            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);\r\n        }}\r\n        .email-header {{\r\n            background-color: #4f46e5;\r\n            padding: 30px;\r\n            text-align: center;\r\n            color: #ffffff;\r\n        }}\r\n        .email-header h1 {{\r\n            margin: 0;\r\n            font-size: 24px;\r\n        }}\r\n        .email-body {{\r\n            padding: 40px 30px;\r\n        }}\r\n        .email-body h2 {{\r\n            color: #1f2937;\r\n            font-size: 20px;\r\n            margin-top: 0;\r\n        }}\r\n        .email-body p {{\r\n            line-height: 1.6;\r\n            margin-bottom: 20px;\r\n        }}\r\n        .btn {{\r\n            display: inline-block;\r\n            background-color: #4f46e5;\r\n            color: #ffffff !important;\r\n            text-decoration: none;\r\n            padding: 12px 25px;\r\n            border-radius: 6px;\r\n            font-weight: bold;\r\n            margin: 20px 0;\r\n            text-align: center;\r\n        }}\r\n        .email-footer {{\r\n            background-color: #f8fafc;\r\n            text-align: center;\r\n            padding: 20px;\r\n            font-size: 12px;\r\n            color: #9ca3af;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"\"email-wrapper\"\">\r\n        <div class=\"\"email-content\"\">\r\n            <!-- Header -->\r\n            <div class=\"\"email-header\"\">\r\n                <h1>მოგესალმებით! 🎉</h1>\r\n            </div>\r\n            \r\n            <!-- Body -->\r\n            <div class=\"\"email-body\"\">\r\n                <h2>გამარჯობა, მეგობარო!</h2>\r\n                <p>ძალიან გვიხარია, რომ შემოგვიერთდი. შენი რეგისტრაცია წარმატებით დასრულდა და უკვე შეგიძლია სრულად ისარგებლო ჩვენი პლატფორმის შესაძლებლობებით.</p>\r\n                <p>დააჭირე ქვემოთ მოცემულ ღილაკს, რომ გადახვიდე შენს პროფილზე და დაიწყო:</p>\r\n                \r\n                <div style=\"\"text-align: center;\"\">\r\n                    <a href=\"\"https://yourwebsite.com/login\"\" class=\"\"btn\"\">პროფილში შესვლა</a>\r\n                </div>\r\n                \r\n                <p>თუ რაიმე შეკითხვა გაქვს, უბრალოდ მოგვწერე ეს მეილი პასუხის სახით – ჩვენი გუნდი მზადაა დაგეხმაროს.</p>\r\n                <p>წარმატებებს გისურვებთ!<br><strong>შენი აპლიკაციის გუნდი</strong></p>\r\n            </div>\r\n            \r\n            <!-- Footer -->\r\n            <div class=\"\"email-footer\"\">\r\n                <p>&copy; 2026 შენი კომპანია. ყველა უფლება დაცულია.</p>\r\n                <p>თუ ეს მეილი შეცდომით მოგივიდა, უბრალოდ უგულებელყავი.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\";");

            _userRepository.SaveUser(user);
        }

        public void VerifyUser(string email, string verificationCode)
        {
            //todo validations 
            if (string.IsNullOrEmpty(email))
            {
                throw new EmailNullexception();
            }
            if (string.IsNullOrWhiteSpace(verificationCode))
            {
                throw new VerificationCodeNullException();
            }
            _userRepository.VerifyUser(email, verificationCode);
        }


        public void LoginUser(string email, string passwiord)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new EmailNullexception();
            }
            if (string.IsNullOrWhiteSpace(passwiord))
            {
                throw new PasswordNullexception();
            }
            /// validations if email or password ar empty
            var userFromFile = _userRepository.GetUserByEmail(email);
            if (BCrypt.Net.BCrypt.Verify(passwiord, userFromFile.Password) && userFromFile.IsVerified)
            {
                Console.WriteLine("Loged In succesfully");
                var users = _userRepository.GetAllUsers();
                List<string> userInStrings = new List<string>();
                foreach (var item in users)
                {
                    if (item.Email == email)
                    {
                        item.IsLogged = true;
                    }
                    userInStrings.Add(item.ToString());
                }
                _userRepository.ChangeFile(userInStrings);
            }
            else
            {
                throw new PasswordNotMatchException();
            }


        }

    }
}
