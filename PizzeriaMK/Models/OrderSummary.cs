namespace PizzeriaMK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderSummary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderSummary()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public int OrderSummaryId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string OrderDate { get; set; }

        [Required]
        [StringLength(255)]
        public string OrderAddress { get; set; }

        public string Note { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(255)]
        public string State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual User User { get; set; }
    }
}
