using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.ViewModels;
using CompanyProject.Models;
using CompanyProject.Controllers;
using CompanyProject.Views;

namespace CompanyProject.ViewModels
{
    class AddEditOrderViewModel : BaseViewModel
    {
        #region Properties
        private List<Item> itemlist;

        private string _showModeVisibility;
        public string ShowModeVisibility
        {
            get { return _showModeVisibility; }
            set { _showModeVisibility = value; NotifyPropertyChanged("ShowModeVisibility"); }
        }
        private bool _isEnabledConfirmButton;
        public bool IsEnabledConfirmButton
        {
            get
            {
                return _isEnabledConfirmButton;
            }
            set
            {
                _isEnabledConfirmButton = value; NotifyPropertyChanged("IsEnabledConfirmButton");
            }
        }

        private bool _showMode;
        public bool ShowMode
        {
            get { return _showMode; }
            set { _showMode = value; NotifyPropertyChanged("ShowMode"); }
        }
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
        public string SelectedOrderId { get; set; }

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
            SelectedOrderId = "";
            ShowModeVisibility = "Visible";
            ShowMode = false;
            editmode = false;
            _editAddLabel = "Add New Order";
            InitializeNewOrder();
        }

        public AddEditOrderViewModel(OrderHeaderView Oh)
        {
            SelectedOrderId = "ID: " + Oh.OrderHeaderId;
            ShowModeVisibility = "Visible";
            ShowMode = false;
            editmode = true;
            _editAddLabel = "Edit Order";
            SelectedOrder = Oh;
            InitializeEditOrder(SelectedOrder);
        }

        public AddEditOrderViewModel(OrderHeaderView Oh, bool showMode)
        {
            SelectedOrderId = "ID: " + Oh.OrderHeaderId;
            ShowModeVisibility = "Hidden";
            editmode = false;
            ShowMode = true;
            _editAddLabel = "Show Order";
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

        public void ValdationChecker()
        {
            IsEnabledConfirmButton = OrdersController.ValidationChecker(itemlist);
        }

        public async Task SaveChanges()
        {
            if (!editmode)
                await OrdersController.AddNewOrder(ItemList, Notes);
            else
                await OrdersController.UpdateOrder(ItemList, SelectedOrder, Notes);
        }

        #endregion
    }
}
