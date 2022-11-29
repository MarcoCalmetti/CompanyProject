using CompanyProject.Models;
namespace CompanyProject.ViewModels
{
    class AddEditResellerViewModel : BaseViewModel
    {
        #region Properties

        private Reseller add_reseller;

        public Reseller AddReseller
        {
            get { return add_reseller; }
            set { add_reseller = value; NotifyPropertyChanged("AddReseller"); }
        }

        private string businessname
            ;

        public string Buisnessname
        {
            get { return businessname; }
            set { businessname = value; NotifyPropertyChanged("BuisnessName"); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; NotifyPropertyChanged("Address"); }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value;NotifyPropertyChanged("City"); }
        }
        private string postalcode;

        public string PostalCode
        {
            get { return postalcode; }
            set { postalcode = value; NotifyPropertyChanged("PostalCode"); }
        }
        private string vat;

        public string VAT
        {
            get { return vat; }
            set { vat = value; NotifyPropertyChanged("VAT"); }
        }

        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; NotifyPropertyChanged("Mail"); }
        }

        private int telephonenumber;

        public int TelephoneNumber
        {
            get { return telephonenumber; }
            set { telephonenumber = value; NotifyPropertyChanged("TelephoneNumber"); }
        }

        


        #endregion

        #region Costructor

        public AddEditResellerViewModel()
        {

        }

        #endregion

        #region Methods

        public void AddUser()
        {
            


        }

        public void EditUser()
        { 
        
        
        }
        
        #endregion

    }
}
