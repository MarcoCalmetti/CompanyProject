using CompanyProject.Models;
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
    public partial class AddEditOrderView : Window
    {
        AddEditOrderViewModel vm;
        public AddEditOrderView()
        {
            vm = new AddEditOrderViewModel();
            DataContext = vm;
            InitializeComponent();
        }

        public AddEditOrderView(OrderHeaderView Oh)
        {
            vm = new AddEditOrderViewModel(Oh);
            DataContext = vm;
            InitializeComponent();
        }

        public AddEditOrderView(OrderHeaderView Oh, bool ShowMode)
        {
            vm = new AddEditOrderViewModel(Oh,true);
            DataContext = vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to save these changes?", "Save", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                vm.SaveChanges();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }

            }
        }


        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            vm.ValdationChecker();
        }
    }
}
