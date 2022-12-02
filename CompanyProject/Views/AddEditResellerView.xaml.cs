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
using System.Windows.Shapes;

namespace CompanyProject.Views
{
    /// <summary>
    /// Logica di interazione per AddEditResellerView.xaml
    /// </summary>
    public partial class AddEditResellerView : Window
    {
        AddEditResellerViewModel vm;
        public AddEditResellerView()
        {
    
            InitializeComponent();
            vm = new AddEditResellerViewModel();
            DataContext = vm;
        }

        public AddEditResellerView(Reseller r)
        {
            vm = new AddEditResellerViewModel(r);
            DataContext = vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.Confirm();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
