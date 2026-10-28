


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Coffe_site.Models
{
    public class Item
    {
        [Key]
        public int Product_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Image { get; set; }

        [JsonIgnore]

        public ICollection<Order> Orders { get; set; }


    }
}
