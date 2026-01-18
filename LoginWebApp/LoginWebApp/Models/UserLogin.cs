using System.ComponentModel.DataAnnotations;

namespace LoginWebApp.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Required Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required Password")]
        public string Password { get; set; }
    }
}
