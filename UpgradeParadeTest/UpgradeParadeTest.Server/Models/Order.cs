using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UpgradeParadeTest.Server.Models
{
    public class Order
    {
        [Required]
        [Key]
        [Column("order_id")]
        public Guid Id { get; set; }
       
        [Column("delivery")]
        public string? DeliveryMethod { get; set; }

        [Column("payment")]
        public string? PaymentMethod { get; set; }
        [Column("total")]
        public decimal TotalPrice { get; set; }

        public List<Product> Products { get; set; }

        [Column("customer_id")]
        [JsonIgnore]
        public Guid CustomerId { get; set; }
        internal Customer Customer { get; set; }

    }
}
