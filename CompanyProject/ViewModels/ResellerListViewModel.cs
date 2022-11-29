using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Controllers;
using CompanyProject.Models;

namespace CompanyProject.ViewModels
{
    class ResellerListViewModel : BaseViewModel
    {


        #region Properties 

        //Creo Le proprietà con cui vado a fare il binding
        private Task<List<Reseller>> list_rivenditori;

        public Task<List<Reseller>> ListResellers // è la lista che mi contiene tutti i reseller
        {
            get { return list_rivenditori; }
            set { list_rivenditori = value; NotifyPropertyChanged("ListResellers"); }
        }

        private Reseller reseller;

        public Reseller SelectedReseller // è il singolo reseller
        {
            get { return reseller; }
            set { reseller = value; NotifyPropertyChanged("SelectedReseller");}
        }

        private string buisnessname;

        public string BuisnessName //è la stringa che vado ad utilizzare nella textbox BuisnessName
        {
            get { return buisnessname; }
            set { buisnessname = value; NotifyPropertyChanged("BuisnessName");  }

        }

        private string vat;

        public string VAT // è la partita iva, stringa che vado ad utilizzare nella textbox VAT
        {
            get { return vat; }
            set { vat = value;NotifyPropertyChanged("VAT");  }
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
            set { city = value; NotifyPropertyChanged(""); }
        }
        private int page;

        public int Page
        {
            get { return page; }
            set { page = value; NotifyPropertyChanged("Page"); }
        }

        private int pagesize = 10;

        public int PageSize
        {
            get { return pagesize = 10; }
            set { pagesize = 10; }
        }

        #endregion

        #region Constructor

        public ResellerListViewModel()
        {
            Page = 1;
            LoadData();
        }
        #endregion

        #region Methods
        public async Task LoadData()
        {
            //await (ListResellers = CompanyController.GetAllResellers(BuisnessName, VAT, SelectedCity, Page, PageSize));
            
        }

        public void ResetFilter()
        {
            BuisnessName = "";
            VAT = "";
            SelectedCity = null;
            LoadData(); 
        }
        #endregion
    }
}