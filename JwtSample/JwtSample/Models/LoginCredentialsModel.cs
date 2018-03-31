using System.ComponentModel.DataAnnotations;

namespace JwtSample
{
    public class LoginCredentialsModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}