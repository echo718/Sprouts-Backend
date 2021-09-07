using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprouts.Models
{
    public enum Year
    {
        YEAR_2021
    }

    public class Study
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public string Language { get; set; } = null!;

        
        public string? ImageURI { get; set; }

        [Required]
        public int KidId { get; set; }

        public Kid Kid { get; set; } = default!;

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

    }
}
