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
    /// Логика взаимодействия для AddRecordPage.xaml
    /// </summary>
    public partial class AddRecordPage : Page
    {
        private ProductSale currentProductSale = new ProductSale();
        Agent currentAgent;
        public AddRecordPage(Agent SelectedAgent)
        {
            InitializeComponent();
            currentAgent = SelectedAgent;
            var currentProduct = Tokarev_GlazkiSaveEntities.GetContext().Product.ToList();
            ProductsComboBox.ItemsSource = currentProduct;
            DataContext = currentProductSale;
        }

        private void ProductsComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProductsComboBox.IsDropDownOpen = true;
            var currentProduct = Tokarev_GlazkiSaveEntities.GetContext().Product.ToList();
            currentProduct = currentProduct.Where(p => p.Title.ToLower().Contains(ProductsComboBox.Text.ToLower())).ToList();
            ProductsComboBox.ItemsSource = currentProduct;
        }

        private void SaveSaleButton_Click(object sender, RoutedEventArgs e)
        {
            var currentProduct = Tokarev_GlazkiSaveEntities.GetContext().Product.ToList();
            currentProductSale.ID = 0;
            currentProductSale.AgentID = currentAgent.ID;
            currentProductSale.ProductID = currentProduct[ProductsComboBox.SelectedIndex].ID;
            currentProductSale.SaleDate = Convert.ToDateTime(ProductSaleDate.Text);
            currentProductSale.ProductCount = Convert.ToInt32(ProductCount.Text);

            Tokarev_GlazkiSaveEntities.GetContext().ProductSale.Add(currentProductSale);
            Tokarev_GlazkiSaveEntities.GetContext().SaveChanges();
            MessageBox.Show("информация сохранена");
            Manager.MainFrame.GoBack();
        }
    }
}
