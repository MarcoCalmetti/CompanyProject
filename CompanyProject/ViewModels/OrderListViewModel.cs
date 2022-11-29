using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;

namespace CompanyProject.ViewModels
{
    class OrderListViewModel : BaseViewModel
    {
        #region Properties

        private List<OrderHeader> list_order;

        public List<OrderHeader> ListOrders
        {
            get { return list_order; }
            set { list_order = value; NotifyPropertyChanged("ListOrders"); }
        }

        private OrderHeader selected_order;

        public OrderHeader SelectedOrder
        {
            get { return selected_order; }
            set { selected_order = value; NotifyPropertyChanged("SelectedOrder"); }
        }

        private string resellernamefilter;

        public string ResellerNameFilter
        {
            get { return resellernamefilter; }
            set { resellernamefilter = value; NotifyPropertyChanged("ResellerNameFilter"); }
        }

        private DateTime filtrodata ;

        public DateTime FiltroData
        {
            get { return filtrodata; }
            set { filtrodata = value; NotifyPropertyChanged("FiltroData"); }
        }

        private List<string> list_status;

        public List<string> ListStatus
        { 
            get { return list_status; }
            set { list_status = value; NotifyPropertyChanged("ListStatus"); }
        }

        private string slected_status;

        public string SelectedStatus
        {
            get { return slected_status; }
            set { slected_status = value; NotifyPropertyChanged("SelectedStatus"); }
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
            set { selected_resellername = value; NotifyPropertyChanged("SelectedResellerName"); }
        }
        private List<int> res_id_list;

        public List<int> ResellersIDList
        {
            get { return res_id_list; }
            set { res_id_list = value; NotifyPropertyChanged("ResellersIDList"); }
        }
        private int selectedid;

        public int SelectedID
        {
            get { return selectedid; }
            set { selectedid = value; NotifyPropertyChanged("SelectedID"); }
        }

        #endregion

        #region Constructor

        public OrderListViewModel()
        {
            LoadData();
        }



        #endregion

        #region Methods
        private void LoadData()
        {
           
        }
        public void AzzeraFiltri()
        {

            ResellerNameFilter = "";
            FiltroData = DateTime.Now;
            SelectedStatus = "";
            SelectedResellerName = "";
       
        }

        #endregion
    }
}
