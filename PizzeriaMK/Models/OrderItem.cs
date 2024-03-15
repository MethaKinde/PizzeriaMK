namespace PizzeriaMK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        public int OrderSummaryId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Min. 1, Max. 10")]
        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public virtual OrderSummary OrderSummary { get; set; }

        public virtual Product Product { get; set; }
    }
}
