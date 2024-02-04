﻿using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Music
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        public string artist { get; set; }

        public int price { get; set; }

        // Other properties...
    }
}
