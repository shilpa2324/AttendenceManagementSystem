using System.ComponentModel.DataAnnotations;
namespace AttendenceManagementSystem.Models
{
    public class LoginDetails
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
