using System.ComponentModel.DataAnnotations;

namespace RealNest.Web.Models.ViewModels.Admins
{
    public class AdminViewModelRegister
    {
        [Required]
        public string AdminName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parollar mos kelmadi.")]
        public string ConfirmPassword { get; set; }
    }
}
