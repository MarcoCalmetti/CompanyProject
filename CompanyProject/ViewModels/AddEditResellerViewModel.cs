using CompanyProject.Controllers;
using CompanyProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CompanyProject.ViewModels
{
    class AddEditResellerViewModel : BaseViewModel
    {
        #region Properties

        private bool EditMode;

        private Reseller add_reseller;

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
            AddReseller = new Reseller();
        }

        
        public AddEditResellerViewModel(Reseller r)
        {
            EditMode = true;
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

        #region INotifyDataErrorInfo



        #endregion

    }
}
