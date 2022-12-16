using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    [MetadataTypeAttribute(typeof(ItemMetadata))]
    public partial class Item : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                ValidationContext valContext = new ValidationContext(new ItemMetadata())
                {
                    MemberName = columnName
                };

                List<ValidationResult> validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this),
                        valContext,
                        validationResults))
                {
                    ErrorChecker = false;
                    return "";
                }

                ErrorChecker = true;

                return validationResults.First().ErrorMessage;

            }
        }

        private string _error;
        public string Error => _error;
        public bool ErrorChecker;
    }


    public partial class ItemMetadata
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public int LeadTime { get; set; }
        [Range(0, 1000000, ErrorMessage = "The quantity must be 0 or more")]
        public int? Quantity { get; set; }

    }
}
