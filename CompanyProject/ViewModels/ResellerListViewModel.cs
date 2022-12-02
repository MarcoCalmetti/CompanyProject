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
    class ResellerListViewModel : BaseViewModel
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
            set { vat = value;NotifyPropertyChanged("VAT"); LoadData(); Page = 1; }
        }

        private List<string> list_city;

        public List<string> ListCity // è la lista di città,
        {
            get { return list_city ; }
            set { list_city = value; NotifyPropertyChanged("ListCity"); }
        }

        private string city;

        public string SelectedCity // è la singola città selezionata
        {
            get { return city; }
            set { city = value; NotifyPropertyChanged("SelectedCity"); LoadData(); Page = 1; }
        }
        private int page;

        public int Page
        {
            get { return page; }
            set { page = value; NotifyPropertyChanged("Page"); }
        }

        private int pagesize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value; }
        }

        #endregion

        #region Constructor

        public ResellerListViewModel()
        {

            Initailize();

        }
        #endregion

        #region Methods

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
            await LoadData();
        }

        public async Task LoadData()
        {
            ListResellers = await ResellersController.GetAll(BusinessName,VAT, SelectedCity, Page, PageSize);
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


        #endregion

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

        internal void checkButton()
        {
            if (Page == 1)
            {
                PreviousPageButtonIsEnabled = false;
                FirstPageButtonIsEnabled = false;
            }
            else
            {
                PreviousPageButtonIsEnabled = true;
                FirstPageButtonIsEnabled = true;
            }

            if (Page >= TotalPages)
            {
                if (TotalPages == 0)
                {
                    Page = 0;
                    PreviousPageButtonIsEnabled = false;
                    FirstPageButtonIsEnabled = false;
                }

                NextPageButtonIsEnabled = false;
                LastPageButtonIsEnabled = false;

            }
            else
            {
                NextPageButtonIsEnabled = true;
                LastPageButtonIsEnabled = true;
            }

        }

        private bool _nextPageButtonIsEnabled;
        public bool NextPageButtonIsEnabled
        {
            get { return _nextPageButtonIsEnabled; }
            set { _nextPageButtonIsEnabled = value; NotifyPropertyChanged("NextPageButtonIsEnabled"); }
        }

        private bool _lastPageButtonIsEnabled;
        public bool LastPageButtonIsEnabled
        {
            get { return _lastPageButtonIsEnabled; }
            set { _lastPageButtonIsEnabled = value; NotifyPropertyChanged("LastPageButtonIsEnabled"); }
        }

        private bool _previousPageButtonIsEnabled;
        public bool PreviousPageButtonIsEnabled

        {
            get { return _previousPageButtonIsEnabled; }
            set { _previousPageButtonIsEnabled = value; NotifyPropertyChanged("PreviousPageButtonIsEnabled"); }
        }

        private bool _firstPageButtonIsEnabled;
        public bool FirstPageButtonIsEnabled

        {
            get { return _firstPageButtonIsEnabled; }
            set { _firstPageButtonIsEnabled = value; NotifyPropertyChanged("FirstPageButtonIsEnabled"); }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; NotifyPropertyChanged("TotalPages"); }
        }
        private string _stringLabelPagina;
        public string StringLabelPagina
        {
            get { return _stringLabelPagina; }
            set { _stringLabelPagina = value; NotifyPropertyChanged("StringLabelPagina"); }
        }
    }
}