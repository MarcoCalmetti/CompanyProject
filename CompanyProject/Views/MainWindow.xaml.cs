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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyProject.Views;


namespace CompanyProject
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new ResellersListView());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new OrdersListView());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new ItemsListView());
        }
        private void NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
