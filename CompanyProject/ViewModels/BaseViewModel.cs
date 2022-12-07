using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.ViewModels
{

    class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        #region IDataErrorInfo

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
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
        #endregion
    }

}
