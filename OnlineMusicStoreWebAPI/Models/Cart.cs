using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStoreWebAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<Music> music { get; set; }

        public int Price { get; set; }
    }
}
