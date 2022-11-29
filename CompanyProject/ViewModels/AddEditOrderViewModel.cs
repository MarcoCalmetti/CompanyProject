using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.ViewModels;
using CompanyProject.Models; 


namespace CompanyProject.ViewModels
{
    class AddEditOrderViewModel : BaseViewModel
    {
        #region Properties
        private List<Item> itemlist;

        public List<Item> ItemList
        {
            get { return itemlist; }
            set { itemlist = value; NotifyPropertyChanged("ItemList"); }
        }

        private Item selecteditem;

        public Item SelectedItem
        {
            get { return selecteditem; }
            set { selecteditem = value; NotifyPropertyChanged("SelectedItem"); }
        }
        #endregion

        #region Constructor
        public AddEditOrderViewModel()
        {

        }
        #endregion

        #region Methods



         
        #endregion
    }
}
