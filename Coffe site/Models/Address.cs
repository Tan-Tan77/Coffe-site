using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Coffe_site.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public int User_Id { get; set; }

        public string Address_Value { get; set; }

        public bool Is_Default { get; set; }

        public string Lat {  get; set; }

        public string Long { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}
