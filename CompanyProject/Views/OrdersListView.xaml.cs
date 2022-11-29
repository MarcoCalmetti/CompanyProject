using CompanyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyProject.Views
{
    /// <summary>
    /// Logica di interazione per OrdersListView.xaml
    /// </summary>
    public partial class OrdersListView : Page
    {
        OrderListViewModel vm;
        public OrdersListView()
        {
            InitializeComponent();
            vm = new OrderListViewModel();
            DataContext = vm;
        }

        private void OrdersList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //EditOrderRightClick();
        }

        private void buttNewOrder_Click(object sender, RoutedEventArgs e)
        {
            //AddOrderRightClick();
        }

        private void bResetFiltersOL_Click(object sender, RoutedEventArgs e)
        {
            vm.AzzeraFiltri();
            //mettere azzerafiltri public nel vm
        }
    }
}
