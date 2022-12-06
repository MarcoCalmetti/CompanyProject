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
    class OrderListViewModel : PaginationViewModel
    {
        #region Properties

        private bool _editModeClickEnabled;
        public bool EditModeClickEnabled
        {
            get { return _editModeClickEnabled; }
            set { _editModeClickEnabled = value; NotifyPropertyChanged("EditModeClickEnabled"); }
        }

        private bool _deleteModeClickEnabled;
        public bool DeleteModeClickEnabled
        {
            get { return _deleteModeClickEnabled; }
            set { _deleteModeClickEnabled = value; NotifyPropertyChanged("DeleteModeClickEnabled"); }
        }

        private bool _startProductionClickEnabled;
        public bool StartProductionClickEnabled
        {
            get { return _startProductionClickEnabled; }
            set { _startProductionClickEnabled = value; NotifyPropertyChanged("StartProductionClickEnabled"); }
        }

        private bool _endProductionClickEnabled;
        public bool EndProductionClickEnabled
        {
            get { return _endProductionClickEnabled; }
            set { _endProductionClickEnabled = value; NotifyPropertyChanged("StartProductionClickEnabled"); }
        }

        private List<OrderHeaderView> list_order;

        public List<OrderHeaderView> ListOrders
        {
            get { return list_order; }
            set { list_order = value; NotifyPropertyChanged("ListOrders");}
        }

        private OrderHeaderView selected_order;

        public OrderHeaderView SelectedOrder
        {
            get { return selected_order; }
            set { selected_order = value; CheckEnabled(); NotifyPropertyChanged("SelectedOrder"); }
        }

        private string resellernamefilter;

        public string ResellerNameFilter
        {
            get { return resellernamefilter; }
            set { resellernamefilter = value; NotifyPropertyChanged("ResellerNameFilter"); LoadData(); Page = 1; }
        }

        private List<string> _orderByList;

        public List<string> OrderByList
        {
            get { return _orderByList; }
            set { _orderByList = value; NotifyPropertyChanged("OrderByList"); }
        }

        private string _selectedOrderBy;

        public string SelectedOrderBy
        {
            get { return _selectedOrderBy; }
            set { _selectedOrderBy = value; NotifyPropertyChanged("SelectedOrderBy"); LoadData(); Page = 1; }
        }


        private List<string> list_status;

        public List<string> ListStatus
        { 
            get { return list_status; }
            set { list_status = value; NotifyPropertyChanged("ListStatus"); }
        }

        private string selected_status;

        public string SelectedStatus
        {
            get { return selected_status; }
            set { selected_status = value; NotifyPropertyChanged("SelectedStatus"); LoadData(); Page = 1; }
        }

        private List<string> list_resellername;

        public List<string> ListResellerName
        { 
            get { return list_resellername; }
            set { list_resellername = value; NotifyPropertyChanged("ListResellerName");}
        }

        private string selected_resellername;

        public string SelectedResellerName
        {
            get { return selected_resellername; }
            set { selected_resellername = value; NotifyPropertyChanged("SelectedResellerName"); Page = 1; }
        }
        private List<int?> res_id_list;

        public List<int?> ResellersIDList
        {
            get { return res_id_list; }
            set { res_id_list = value; NotifyPropertyChanged("ResellersIDList"); }
        }
        private int? selectedid;

        public int? SelectedID
        {
            get { return selectedid; }
            set { selectedid = value; NotifyPropertyChanged("SelectedID"); LoadData(); Page = 1; }
        }

        #endregion

        #region Constructor

        public OrderListViewModel()
        {
            Initialize();
        }

        #endregion

        #region Methods
        private async Task Initialize()
        {
            EditModeClickEnabled = true;
            Page = 1;
            PageSize = 10;
            OrderByList = new List<string>() {"Reseller Name", "OrderDate" };
            ResellersIDList = await OrdersController.ResellersIDList();
            ListStatus = await OrdersController.OrderStatusList();
            LoadData();
        }

        private async Task LoadData()
        {
            _totalPages = (int)Math.Ceiling(await OrdersController.GetItemsNumber(ResellerNameFilter, SelectedStatus, SelectedID) / (double)PageSize);
            ListOrders = await OrdersController.GetAll(ResellerNameFilter, SelectedStatus, SelectedID, SelectedOrderBy, Page, PageSize);
            checkButton();
            StringLabelPagina = "Page " + page + " of " + _totalPages;
            CheckEnabled();
        }
        public void AzzeraFiltri()
        {
            ResellerNameFilter = null;
            SelectedStatus = null;
            SelectedResellerName = null;
            SelectedOrderBy = null;
            SelectedID = null;
        }

        public async Task AddOrder()
        {
            AddEditOrderView AEO = new AddEditOrderView();
            AEO.ShowDialog();
            await Initialize();
        }

        public async Task EditOrder()
        {
            AddEditOrderView AEO = new AddEditOrderView(SelectedOrder);
            AEO.ShowDialog();
            await Initialize();
        }

        public async Task DeleteOrder()
        {
            await OrdersController.DeleteOrder(SelectedOrder);
            await Initialize();
        }

        public void CheckEnabled()
        {
            if(SelectedOrder != null)
            {
                EditModeClickEnabled = SelectedOrder.OrderStatusString == "Confermato" && SelectedOrder.ResellerId == null;
                DeleteModeClickEnabled = SelectedOrder.OrderStatusString == "Confermato" && SelectedOrder.ResellerId == null;
                StartProductionClickEnabled = SelectedOrder.OrderStatusString == "Confermato";
                EndProductionClickEnabled = SelectedOrder.OrderStatusString == "InProduzione";
            }
            
        }

        public async Task StartProduction()
        {
            await OrdersController.StartProduction(SelectedOrder);
            await Initialize();
        }

        public async Task EndProduction()
        {
            await OrdersController.EndProduction(SelectedOrder);
            await Initialize();
        }

        #endregion
        #region Pagination



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
