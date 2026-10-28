using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_site.Models
{
    public class Cart
    {


        public int id { get; set; }


        public int count { get; set; } = 1;

        public int product_Id { get; set; }

        [ForeignKey("product_Id")]

        public virtual Item? item { get; set; }

        public int? user_Id { get; set; }
        [ForeignKey("user_Id")]
        public virtual User? user { get; set; }

    }
}
