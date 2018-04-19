using System.ComponentModel.DataAnnotations;

namespace VirtualClassroom.Authentication.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
