using System.ComponentModel.DataAnnotations;

namespace VirtualClassroom.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
