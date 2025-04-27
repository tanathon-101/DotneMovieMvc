using System;
using System.ComponentModel.DataAnnotations;

namespace MovieMvc.Models
{
    public class Movies
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string CoverImg { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Genre { get; set; }
        [Range(1, 1000, ErrorMessage = "Duration must be between 1 and 1000 minutes")]

        public int Duration { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
