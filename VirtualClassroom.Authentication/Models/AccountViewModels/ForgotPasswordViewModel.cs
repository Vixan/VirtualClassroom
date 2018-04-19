using System.ComponentModel.DataAnnotations;

namespace VirtualClassroom.Authentication.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
