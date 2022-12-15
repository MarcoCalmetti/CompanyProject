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
                    return "";

                return validationResults.First().ErrorMessage;
            }
        }

        private string _error;
        public string Error => _error;

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
    }

}
