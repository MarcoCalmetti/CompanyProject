using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class OrderHeaderView : OrderHeader
    {
        public double? TotalPrice { get; set; }
        public int? Leadtime { get; set; }
        public string BusinessName { get; set; }
        public string OrderStatusString { get; set; }

    }
}
