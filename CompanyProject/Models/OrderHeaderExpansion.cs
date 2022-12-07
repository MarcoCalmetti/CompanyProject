using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class OrderHeaderView : OrderHeader
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        public double? TotalPrice { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int? Leadtime { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string OrderStatusString { get; set; }

    }
}
