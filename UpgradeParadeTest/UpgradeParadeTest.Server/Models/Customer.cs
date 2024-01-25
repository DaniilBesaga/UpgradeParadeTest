using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;

namespace UpgradeParadeTest.Server.Models
{
    public class Customer
    {
        [Required]
        [Key]
        [Column("customer_id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
    }
}
