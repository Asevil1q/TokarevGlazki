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

namespace TokarevGlazki
{
    /// <summary>
    /// Логика взаимодействия для SalesHistory.xaml
    /// </summary>
    public partial class SalesHistory : Page
    {
        Agent currentAgent;
        public SalesHistory(Agent SelectedAgent)
        {

            InitializeComponent();
            currentAgent = SelectedAgent;
            var Sales = Tokarev_GlazkiSaveEntities.GetContext().ProductSale.ToList();
            if (SelectedAgent.ID != 0)
            {
                Sales = Sales.Where(p => p.AgentID == SelectedAgent.ID).ToList();
            }
            Sales_Listview.ItemsSource = Sales;

            DeleteButton.Visibility = Visibility.Collapsed;
        }

        private void Update_Sales()
        {
            var Sales = Tokarev_GlazkiSaveEntities.GetContext().ProductSale.ToList();
            if (currentAgent.ID != 0)
            {
                Sales = Sales.Where(p => p.AgentID == currentAgent.ID).ToList();
            }
            Sales_Listview.ItemsSource = Sales;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update_Sales();
        }

        private void Sales_Listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sales_Listview.SelectedItems.Count == 0)
                DeleteButton.Visibility = Visibility.Collapsed;
            if (Sales_Listview.SelectedItems.Count > 0)
                DeleteButton.Visibility = Visibility.Visible;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRecordPage(currentAgent));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            List<ProductSale> SelectedSales = Sales_Listview.SelectedItems.Cast<ProductSale>().ToList();
            foreach(ProductSale Sale in SelectedSales)
            {
                Tokarev_GlazkiSaveEntities.GetContext().ProductSale.Remove(Sale);
            }
            Tokarev_GlazkiSaveEntities.GetContext().SaveChanges();
            Update_Sales();
        }
    }
}
