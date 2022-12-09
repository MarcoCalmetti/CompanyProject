using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    [MetadataTypeAttribute(typeof(ResellerMetaData))]
    public partial class ResellerExtension
    {
    }

    [DataContract]
    public class ResellerMetaData
    {
        [DataMember]
        public int ResellerID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string BusinessName { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string VAT { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Address { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string City { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string PostalCode { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Mail { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string TelephoneNumber { get; set; }
        //public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
        //questo in teoria non serve validarlo, provare
    }
}
