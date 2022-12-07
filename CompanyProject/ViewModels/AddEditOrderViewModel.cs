using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.ViewModels;
using CompanyProject.Models;
using CompanyProject.Controllers;
using CompanyProject.Views;
using System.Collections;
using System.ComponentModel;

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

        private OrderHeaderView selectedOrder;

        public OrderHeaderView SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; NotifyPropertyChanged("SelectedOrder"); }
        }

        private string _editAddLabel;
        public string EditAddLabel
        {
            get { return _editAddLabel; }
            set { _editAddLabel = value; NotifyPropertyChanged("EditAddLabel"); }
        }


        private string note;

        public string Notes
        {
            get { return note; }
            set { note = value; NotifyPropertyChanged("Notes"); }
        }

        private bool editmode;

        #endregion

        #region Constructor
        public AddEditOrderViewModel()
        {
            editmode = false;
            _editAddLabel = "Add New Order";
            InitializeNewOrder();
        }

        public AddEditOrderViewModel(OrderHeaderView Oh)
        {
            editmode = true;
            _editAddLabel = "Edit Order";
            SelectedOrder = Oh;
            InitializeEditOrder(SelectedOrder);
        }
        #endregion

        #region Methods
        public async Task InitializeEditOrder(OrderHeaderView Oh)
        {
            Notes = Oh.Note;
            ItemList = await OrdersController.GetAllOrdersRows(Oh);
        }

        public async Task InitializeNewOrder()
        {
            ItemList = await OrdersController.GetAllOrdersRows();
        }

        public async Task SaveChanges()
        {
            if (!editmode)
                await OrdersController.AddNewOrder(ItemList, Notes);
            else
                await OrdersController.UpdateOrder(ItemList, SelectedOrder, Notes);
        }

        #endregion

        #region IDataErrorInfo



        #endregion

    }
}
