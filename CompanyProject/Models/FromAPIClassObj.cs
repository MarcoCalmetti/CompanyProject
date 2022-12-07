using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class OrderItem
    {
        public string OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double UnitaryPrice { get; set; }
    }

    public class OrderAPI
    {
        public string Id { get; set; }
        public string ResellerId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStateId { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime StartProductionDate { get; set; }
        public DateTime StopProductionDate { get; set; }
        public string Notes { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }


}
