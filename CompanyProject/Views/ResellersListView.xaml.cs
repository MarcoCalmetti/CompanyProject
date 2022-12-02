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
using CompanyProject.ViewModels;

namespace CompanyProject.Views
{
    /// <summary>
    /// Logica di interazione per ResellersListView.xaml
    /// </summary>
    public partial class ResellersListView : Page
    {
        ResellerListViewModel vm;

        
        public ResellersListView()
        {

            InitializeComponent();
            vm = new ResellerListViewModel();
            DataContext = vm;
        }

        private void ResellersList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //EditResellerRightClick();
        }

        private void buttNewReseller_Click(object sender, RoutedEventArgs e)
        {
            //AddResellerRightClick();
        }

        private void bResetFiltersRL_Click(object sender, RoutedEventArgs e)
        {
            vm.ResetFilter();
        }
    }
}
