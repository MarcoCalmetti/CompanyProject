using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    [MetadataTypeAttribute(typeof(OrderHeaderMetaData))]
    public partial class OrderHeader
    {
    }

    [DataContract]
    public class OrderHeaderMetaData
    {
        [DataMember]
        public int OrderHeaderId { get; set; }
        [DataMember]
        public Nullable<int> ResellerId { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [DataMember]
        public Nullable<int> OrderStatus { get; set; }
        [DataMember]
        public System.DateTime OrderReceipt { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ProductionStartDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ProductionEndDate { get; set; }
        [DataMember]
        public Nullable<int> SalesOrderReference { get; set; }
        [DataMember]
        public string Note { get; set; }

        //public virtual OrderState OrderState { get; set; }//virtual?
        //public virtual Reseller Reseller { get; set; }//virtual?
        //public virtual ICollection<OrderRow> OrderRows { get; set; }
        //questo in teoria non serve validarlo, provare

    }

}
