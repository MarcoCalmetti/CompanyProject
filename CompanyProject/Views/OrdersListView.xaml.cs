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
            vm.AddOrder();
        }

        private void bResetFiltersOL_Click(object sender, RoutedEventArgs e)
        {
            vm.AzzeraFiltri();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.FirstPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.PreviousPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.NextPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            vm.LastPage();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            vm.EditOrder();
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Order?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                vm.DeleteOrder();

        }

        private void StartProduction_Click(object sender, RoutedEventArgs e)
        {
            vm.StartProduction();
        }


        private void EndProduction_Click(object sender, RoutedEventArgs e)
        {
            vm.EndProduction();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
