using System.ComponentModel.DataAnnotations;

namespace OnlineMusicStore.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<Music> music { get; set; }

        public int Price { get; set; }
    }
}
