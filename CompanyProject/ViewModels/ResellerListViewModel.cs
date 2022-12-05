using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Controllers;
using CompanyProject.Models;
using CompanyProject.Views;

namespace CompanyProject.ViewModels
{
    class ResellerListViewModel : PaginationViewModel
    {

        #region Properties 

        //Creo Le proprietà con cui vado a fare il binding
        private List<Reseller> list_rivenditori;

        public List<Reseller> ListResellers // è la lista che mi contiene tutti i reseller
        {
            get { return list_rivenditori; }
            set { list_rivenditori = value; NotifyPropertyChanged("ListResellers"); }
        }

        private Reseller reseller;

        public Reseller SelectedReseller // è il singolo reseller
        {
            get { return reseller; }
            set { reseller = value; NotifyPropertyChanged("SelectedReseller"); }
        }

        private string businessname;

        public string BusinessName //è la stringa che vado ad utilizzare nella textbox BuisnessName
        {
            get { return businessname; }
            set { businessname = value; NotifyPropertyChanged("BusinessName"); LoadData(); Page = 1; }

        }

        private string vat;

        public string VAT // è la partita iva, stringa che vado ad utilizzare nella textbox VAT
        {
            get { return vat; }
            set { vat = value; NotifyPropertyChanged("VAT"); LoadData(); Page = 1; }
        }

        private List<string> list_city;

        public List<string> ListCity // è la lista di città,
        {
            get { return list_city; }
            set { list_city = value; NotifyPropertyChanged("ListCity"); }
        }

        private string city;

        public string SelectedCity // è la singola città selezionata
        {
            get { return city; }
            set { city = value; NotifyPropertyChanged("SelectedCity"); LoadData(); Page = 1; }
        }



        #endregion

        #region Constructor

        public ResellerListViewModel()
        {

            Initailize();

        }
        #endregion

        #region Methods
        internal async Task RemoveReseller()
        {
            await ResellersController.Delete(SelectedReseller);
            Initailize();
        }

        internal void EditReseller()
        {
            AddEditResellerView ADR = new AddEditResellerView(SelectedReseller);
            ADR.ShowDialog();
            Initailize();
        }

        internal void AddReseller()
        {
            AddEditResellerView ADR = new AddEditResellerView();
            ADR.ShowDialog();
            Initailize();
        }
        public async Task Initailize()
        {
            PageSize = 10;
            Page = 1;
            ListCity = await ResellersController.GetAllCity();
            LoadData();
        }

        public async Task LoadData()
        {
            ListResellers = await ResellersController.GetAll(BusinessName, VAT, SelectedCity, Page, PageSize);
            _totalPages = (int)Math.Ceiling(await ResellersController.GetResellerNumber(BusinessName, VAT, SelectedCity) / (double)PageSize);
            checkButton();
            StringLabelPagina = "Page " + page + " of " + _totalPages;
        }

        public async Task ResetFilter()
        {
            businessname = "";
            vat = "";
            city = null;
            NotifyPropertyChanged("SelectedCity");
            NotifyPropertyChanged("VAT");
            NotifyPropertyChanged("BusinessName");
            LoadData();
        }

        internal void PreviousPage()
        {
            Page--;
            checkButton();
            LoadData();
        }

        internal void FirstPage()
        {
            Page = 1;
            LoadData();
        }

        internal void LastPage()
        {
            Page = TotalPages;
            checkButton();
            LoadData();
        }

        internal void NextPage()
        {

            Page++;
            checkButton();
            LoadData();

        }


        #endregion

    }
}