using AnyarMVC.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyarMVC.Models
{
    public class Team:BaseEntity
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
