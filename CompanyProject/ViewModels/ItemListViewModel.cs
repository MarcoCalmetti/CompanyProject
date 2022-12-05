using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;
using CompanyProject.Controllers;


namespace CompanyProject.ViewModels
{
    class ItemListViewModel : PaginationViewModel
    {
        #region Proprerties

        private List<Item> list_items;

        public List<Item> ListItems
        {
            get { return list_items; }
            set { list_items = value; NotifyPropertyChanged("ListItems"); }
        }

        //Creare forse oggetto items? 

        private string filtro_name;

        public string FilterName
        {
            get { return filtro_name; }
            set { filtro_name = value; NotifyPropertyChanged("FilterName"); LoadData(); Page = 1; }
        }

        private string filtro_itemcode;

        public string FilterItemCode
        {
            get { return filtro_itemcode; }
            set { filtro_itemcode = value; NotifyPropertyChanged("FilterItemCode"); LoadData(); Page = 1; }
        }

        private float min_price;

        public float MinPrice
        {
            get { return min_price; }
            set { min_price = value; NotifyPropertyChanged("MinPrice"); }

        }

        private float max_price;

        public float MaxPrice
        {
            get { return max_price; }
            set { max_price = value; NotifyPropertyChanged("MaxPrice"); }
        }

        #endregion

        #region Constructor

        public ItemListViewModel()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private async Task Initialize()
        {
            Page = 1;
            PageSize = 10;
            
            LoadData();
        }

        public async Task LoadData()
        {
            _totalPages = (int)Math.Ceiling(await ItemsController.GetItemsNumber(FilterName, FilterItemCode)/ (double)PageSize);
            ListItems = await ItemsController.GetAll(FilterName, FilterItemCode, Page, PageSize);
            checkButton();
            StringLabelPagina = "Page " + page + " of " + _totalPages;
        }
        public void ResetFilters()
        {
            filtro_name = null;
            filtro_itemcode = null;
            NotifyPropertyChanged("FilterName");
            NotifyPropertyChanged("FilterItemCode");
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
