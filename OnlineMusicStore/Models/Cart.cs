﻿using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Cart
    {
        public int id { get; set; }

        public List<Music> music { get; set; }

        public int price { get; set; }
    }
}
