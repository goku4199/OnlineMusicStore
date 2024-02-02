using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Music
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        public string Artist { get; set; }

        public int Price { get; set; }

        // Other properties...
    }
}
