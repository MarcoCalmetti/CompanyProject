using CompanyProject.Controllers;
using CompanyProject.Models;
using System;

namespace CompanyProject.ViewModels
{
    class AddEditResellerViewModel : BaseViewModel
    {
        #region Properties

        private bool EditMode;

        private Reseller add_reseller;

        private string _idLabelVisibility;

        public string IdLabelVisibility
        {
            get { return _idLabelVisibility; }
            set { _idLabelVisibility = value; }
        }

        private string _editNewResellerString;
        public string EditNewResellerString
        {
            get { return _editNewResellerString; }
            set { _editNewResellerString = value; }
        }
        public Reseller AddReseller
        {
            get { return add_reseller; }
            set { add_reseller = value; NotifyPropertyChanged("AddReseller"); }
        }

        #endregion

        #region Costructor

        public AddEditResellerViewModel()
        {
            EditMode = false;
            EditNewResellerString = "Add Reseller";
            _idLabelVisibility = "";
            AddReseller = new Reseller();
        }

        public AddEditResellerViewModel(Reseller r)
        {
            _idLabelVisibility = "ID: " + r.ResellerID;
            EditMode = true;
            EditNewResellerString = "Edit Reseller";
            AddReseller = new Reseller();
            AddReseller.ResellerID = r.ResellerID;
            AddReseller.Address = r.Address;
            AddReseller.City = r.City;
            AddReseller.VAT = r.VAT;
            AddReseller.BusinessName = r.BusinessName;
            AddReseller.Mail = r.Mail;
            AddReseller.PostalCode = r.PostalCode;
            AddReseller.TelephoneNumber = r.TelephoneNumber;

        }
        #endregion

        #region Methods

        internal void Confirm()
        {
            if (EditMode)
                ResellersController.Update(AddReseller);
            else
                ResellersController.Add(AddReseller);
        }



        #endregion

    }
}
