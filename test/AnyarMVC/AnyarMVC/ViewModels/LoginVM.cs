using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AnyarMVC.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
