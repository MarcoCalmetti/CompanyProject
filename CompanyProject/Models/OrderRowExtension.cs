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
    [MetadataTypeAttribute(typeof(OrderRowMetaData))]
    public partial class OrderRowExtension : IDataErrorInfo
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

    public class OrderRowMetaData
    {
        public int OrderRowId { get; set; }
        public Nullable<int> OrderHeaderId { get; set; }
        public Nullable<int> ItemId { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public double UnitPrice { get; set; }
    }
}
