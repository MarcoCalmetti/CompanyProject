using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    [MetadataTypeAttribute(typeof(ResellerMetaData))]
    public partial class Reseller : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                ValidationContext valContext = new ValidationContext(new ResellerMetaData())
                {
                    MemberName = columnName
                };

                List<ValidationResult> validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this),
                        valContext,
                        validationResults))
                    return "";

                return validationResults.First().ErrorMessage;
            }
        }

        private string _error;
        public string Error => _error;

    }

    public class ResellerMetaData
    {//grandezze uguali al db
        public int ResellerID { get; set; }
        [MaxLength(1000, ErrorMessage = "Max 1000 graphics")]
        [Required(ErrorMessage = "Required Field")]//max 1000 caratteri
        public string BusinessName { get; set; }
        [StringLength(11)]
        [Required(ErrorMessage = "Required Field")]//11 caratteri
        public string VAT { get; set; }
        [MaxLength(100, ErrorMessage = "Max 100 graphics")]
        [Required(ErrorMessage = "Required Field")]//max 100 caratteri
        public string Address { get; set; }
        [MaxLength(100, ErrorMessage = "Max 100 graphics")]
        [Required(ErrorMessage = "Required Field")]//max 100 caratteri
        public string City { get; set; }
        [MaxLength(100, ErrorMessage = "Max 100 graphics")]
        [Required(ErrorMessage = "Required Field")]//max 100 caratteri
        public string PostalCode { get; set; }
        [MaxLength(100, ErrorMessage = "Max 100 graphics")]
        [Required(ErrorMessage = "Required Field")]//max 100 caratteri
        public string Mail { get; set; }
        [MaxLength(100, ErrorMessage = "Max 100 graphics")]
        [Required(ErrorMessage = "Required Field")]//max 100 caratteri
        public string TelephoneNumber { get; set; }
    }
}
