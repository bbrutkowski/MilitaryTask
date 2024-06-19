using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MilitaryTask.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(45)]
        public string OrderId { get; set; }
        public int? ErpOrderId { get; set; }
        public int? InvoiceId { get; set; }
        public int? StoreId { get; set; }
    }
}
