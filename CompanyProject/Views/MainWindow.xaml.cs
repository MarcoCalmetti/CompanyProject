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
        private bool MinimizeWindow;

        public MainWindow()
        {
            MinimizeWindow = true;
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
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // fa in modo che premendo in quasi ogni punto della window possa spostarla 
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }

            }
        }

        private void Button_Click_Resize(object sender, RoutedEventArgs e)
        { //ingrandisce la finestra o la normalizza
            MinimizeWindow = !MinimizeWindow;
            if (!MinimizeWindow)
            { 
                this.WindowState = WindowState.Maximized;
                this.Min_Max.Content = "↙";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.Min_Max.Content = "↗";
            }
                
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {//minimizza la finestra ad icona
            this.WindowState = WindowState.Minimized;
        }
    }
}
