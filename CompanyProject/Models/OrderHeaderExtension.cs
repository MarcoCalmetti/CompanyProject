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
                ValidationContext valContext = new ValidationContext(new OrderHeaderMetaData())
                {
                    MemberName = columnName
                };

                List<ValidationResult> validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this),
                        valContext,
                        validationResults))
                {
                    ErrorChecker[columnName] = false;
                    return "";
                }
                if (!ErrorChecker.ContainsKey(columnName))
                    ErrorChecker.Add(columnName, true);
                else
                    ErrorChecker[columnName] = true;
                return validationResults.First().ErrorMessage;

            }
        }

        private string _error;
        public string Error => _error;

        public Dictionary<string, bool> ErrorChecker = new Dictionary<string, bool>();
    }

    public class OrderHeaderMetaData
    {//grandezze uguali al db
        public int OrderHeaderId { get; set; }
        public Nullable<int> ResellerId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public System.DateTime OrderReceipt { get; set; }
        public Nullable<System.DateTime> ProductionStartDate { get; set; }
        public Nullable<System.DateTime> ProductionEndDate { get; set; }
        public Nullable<int> SalesOrderReference { get; set; }
        [MaxLength(100, ErrorMessage = "Max 100 graphics")]
        public string Note { get; set; }//max 100, non è required
    }
}
