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
    public partial class Reseller
    {
    }

    public class ResellerMetaData
    {
        public int ResellerID { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        //[Range(0,float.MaxValue,ErrorMessage ="Importo negativo")]??
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string VAT { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string City { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string TelephoneNumber { get; set; }
    }
}
