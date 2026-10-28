using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
namespace Coffe_site.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public decimal Delivery_Price { get; set; } = 0;

        public bool Pickup { get; set; } = false;

        public int Count { get; set; } = 1;


        public int? address_Id { get; set; }

        [ForeignKey("address_Id")]
        [JsonIgnore]
        public  Address? address { get; set; }


       
        public  int? item_id { get; set; }

        [ForeignKey("item_id")]
        [JsonIgnore]
        public  Item? item { get; set; }

        public int? user_id { get; set; }

        [ForeignKey("user_id")]

        public  User? user { get; set; }

    }
}