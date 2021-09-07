using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprouts.Models
{
    public class Kid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Age { get; set; }

        [Required]
        public string GitHub { get; set; }

        public string ImageURI { get; set; }

        public ICollection<Study> Studies { get; set; } = new List<Study>();
    }
}
