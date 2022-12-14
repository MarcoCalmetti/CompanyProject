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
    [MetadataTypeAttribute(typeof(OrderHeaderMetaData))]
    public partial class OrderHeader : IDataErrorInfo
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

    public class OrderHeaderMetaData
    {
        public int OrderHeaderId { get; set; }
        public Nullable<int> ResellerId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public System.DateTime OrderReceipt { get; set; }
        public Nullable<System.DateTime> ProductionStartDate { get; set; }
        public Nullable<System.DateTime> ProductionEndDate { get; set; }
        public Nullable<int> SalesOrderReference { get; set; }
        public string Note { get; set; }
    }

}
