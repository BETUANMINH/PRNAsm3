using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }

    public class LoginUserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
