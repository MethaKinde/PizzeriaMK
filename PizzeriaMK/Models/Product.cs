namespace PizzeriaMK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Max 255 caratteri")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Max 1000 caratteri")]
        public string ProductImage { get; set; }

        [Required]
        [Range(1, 99, ErrorMessage = "Scegli un prezzo da 1� a 99�")]
        public decimal ProductPrice { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 caratteri")]
        public string PreparationTime { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Max 1000 caratteri")]
        public string Ingredients { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Devi scegliere una categoria!")]
        public string Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
