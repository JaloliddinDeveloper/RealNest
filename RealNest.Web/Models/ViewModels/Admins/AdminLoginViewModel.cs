using System.ComponentModel.DataAnnotations;

namespace RealNest.Web.Models.ViewModels.Admins
{
    public class AdminLoginViewModel
    {
        [Required]
        public string AdminName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
