using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UpgradeParadeTest.Server.Models
{
    public class Product
    {
        [Required]
        [Key]
        [Column("product_id")]
        public Guid Id { get; set; }

        [Column("product_price")]

        public decimal ProductPrice { get; set; }

        [MaxLength(150)]
        [Column("product_name")]
        public string? ProductName { get; set; }
        
        [Column("product_img")]
        public string? ProductImg { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
    }
}
