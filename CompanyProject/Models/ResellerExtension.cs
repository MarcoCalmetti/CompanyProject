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
                ValidationContext valContext = new ValidationContext(this)
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
    {
        public int ResellerID { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
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
