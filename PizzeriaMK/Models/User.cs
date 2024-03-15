namespace PizzeriaMK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    [Authorize(Roles = "admin")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Max 255 caratteri")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Max 16 caratteri/numeri")]
        public string Password { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Max 30 caratteri")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Max 100 caratteri")]
        public string Surname { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Role { get; set; } = "user";

        //RELAZIONI

        public ICollection<OrderSummary> OrderSummaries { get; set; }
    }
}
