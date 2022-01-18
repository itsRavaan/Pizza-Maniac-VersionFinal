using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos
{
    public class PizzaDto
    {
        [Key] //Sets as PK and sets to Identity
        public int Id { get; set; }

        [Required]
        public string PizzaName { get; set; }

        [Required]
        public string PizzaText { get; set; }

        [Required]
        public double PizzaPrice { get; set; }

        public string PizzaMarketingImage { get; set; }
    }
}
