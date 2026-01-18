using System.Security.Cryptography;
using System.Text;
using LoginWebApp.DAL;
using LoginWebApp.Models;

namespace LoginWebApp.BLL
{
    public class AuthService
    {
        private readonly UserRepository _repo;

        public AuthService(UserRepository repo)
        {
            _repo = repo;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            string hash = ComputeHash(password.Trim());
            return _repo.Login(username.Trim(), hash);
        }


        private string ComputeHash(string input)
        {
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLower();
        }


        public void Register(UserRegister model)
        {
            string hash = ComputeHash(model.Password);

            User user = new User
            {
                FullName = model.FullName,
                Username = model.Username,
                Email = model.Email,
                Role = 1,      // 1 = Standard
                IsActive = true
            };

            _repo.Register(user, hash);
        }

    }


}
