using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    [MetadataTypeAttribute(typeof(OrderRowMetaData))]
    public partial class OrderRowExtension
    {
    }

    [DataContract]
    public class OrderRowMetaData
    {
        [DataMember]
        public int OrderRowId { get; set; }
        [DataMember]
        public Nullable<int> OrderHeaderId { get; set; }
        [DataMember]
        public Nullable<int> ItemId { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Quantity { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public double UnitPrice { get; set; }

        //public virtual Item Item { get; set; } //virtual?
        //public virtual OrderHeader OrderHeader { get; set; }//virtual?
    }
}
