using System.ComponentModel.DataAnnotations;

namespace JwtSample
{
    public class UserRegistrationModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
