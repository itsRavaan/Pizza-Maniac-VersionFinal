using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euromonitor.Models
{
    public class AppUserOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AppUserId { get; set; }

        [Required]
        public int PizzaId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public string OrderPizzaName { get; set; }

        [Required]
        public double PizzaPurchasePrice { get; set; }

        public DateTime UnorderOrderDate { get; set; }

        public int OrderIsDeleted { get; set; }
    }
}
