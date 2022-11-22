using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;


namespace CompanyProject.ViewModels
{
    class ItemListViewModel : BaseViewModel
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
            set { filtro_name = value; NotifyPropertyChanged("FilterName"); }
        }

        private string filtro_itemcode;

        public string FilterItemCode
        {
            get { return filtro_itemcode; }
            set { filtro_itemcode = value; NotifyPropertyChanged("FilterItemCode"); }
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
            LoadData();
        }



        #endregion

        #region Methods
        public void LoadData()
        {
           
        }
        public void ResetFilters()
        {
            FilterName = null;
            FilterItemCode = null;
            MinPrice = 0;
            MaxPrice = 0;
            LoadData();
        
        }

        #endregion
    

    }
}
