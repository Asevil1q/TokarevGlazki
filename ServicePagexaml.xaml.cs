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
    /// Логика взаимодействия для ServicePagexaml.xaml
    /// </summary>
    public partial class ServicePagexaml : Page
    {
        public ServicePagexaml()
        {
            InitializeComponent();
            var currentAgent = Tokarev_GlazkiSaveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgent;
        }
        private void UpdatePage()
        {
            var currentGlazki = Tokarev_GlazkiSaveEntities.GetContext().Agent.ToList();
            currentGlazki = currentGlazki.Where(p => (p.Title.ToLower().Contains(TBSearch.Text.ToLower()))).ToList();

            if (Sortirovka.SelectedIndex == 1)
            {
                currentGlazki = currentGlazki.OrderBy(p => p.Title).ToList();
            }
            if (Sortirovka.SelectedIndex == 5)
            {
                currentGlazki = currentGlazki.OrderBy(p => p.Priority).ToList();
            }
            if (Filtraciya.SelectedIndex == 0)
            {
                currentGlazki = currentGlazki.Where(p => (p.AgentTypeString == "МФО")).ToList();
            }
            if (Filtraciya.SelectedIndex == 1)
            {
                currentGlazki = currentGlazki.Where(p => (p.AgentTypeString == "ЗАО")).ToList();
            }
            if (Filtraciya.SelectedIndex == 2)
            {
                currentGlazki = currentGlazki.Where(p => (p.AgentTypeString == "МКК")).ToList();
            }
            if (Filtraciya.SelectedIndex == 3)
            {
                currentGlazki = currentGlazki.Where(p => (p.AgentTypeString == "ОАО")).ToList();
            }
            if (Filtraciya.SelectedIndex == 4)
            {
                currentGlazki = currentGlazki.Where(p => (p.AgentTypeString == "ООО")).ToList();
            }
            if (Filtraciya.SelectedIndex == 5)
            {
                currentGlazki = currentGlazki.Where(p => (p.AgentTypeString == "ПАО")).ToList();
            }
            AgentListView.ItemsSource = currentGlazki;
            if (Sortirovka.SelectedIndex == 0)
            {
                currentGlazki = currentGlazki.OrderByDescending(p => p.Title).ToList();
            }
            if (Sortirovka.SelectedIndex == 4)
            {
                currentGlazki = currentGlazki.OrderByDescending(p => p.Priority).ToList();
            }
            AgentListView.ItemsSource = currentGlazki;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePage();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePage();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePage();
        }

        private void Sortirovka_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private void Filtraciya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePage();
        }
    }
}
