using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
    }
}