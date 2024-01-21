using System.ComponentModel.DataAnnotations;

namespace AnyarMVC.Areas.Admin.ViewModels
{
    public class UpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
