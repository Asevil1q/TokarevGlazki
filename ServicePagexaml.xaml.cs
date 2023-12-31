﻿using System;
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
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;


        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;



        public ServicePagexaml()
        {
            InitializeComponent();
            var currentAgent = Tokarev_GlazkiSaveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgent;
            TableList = currentAgent.ToList();
            UpdatePage();
        }

        private void UpdatePage()
        {
            var currentGlazki = Tokarev_GlazkiSaveEntities.GetContext().Agent.ToList();
            currentGlazki = currentGlazki.Where(p =>(p.Title.ToLower().Contains(TBSearch.Text.ToLower()))).ToList();

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
            TableList = currentGlazki;
            ChangePage(0, 0);
          
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
        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;
            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }
            Boolean Ifupdate = true;
            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;
                
                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Tokarev_GlazkiSaveEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentListView.ItemsSource = Tokarev_GlazkiSaveEntities.GetContext().Agent.ToList();
            }
            UpdatePage();
        }

 

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SalesHistory((sender as Button).DataContext as Agent));
        }

        private void ChangePriorityButton_Click(object sender, RoutedEventArgs e)
        {
            int max = 0;
            foreach (Agent agent in AgentListView.SelectedItems)
            {
                if (agent.Priority >= max)
                {
                    max = agent.Priority;
                }
            }
            Window1 window = new Window1(max);
            window.ShowDialog();
            if (string.IsNullOrEmpty(window.PriorityTB.Text)) return;
            MessageBox.Show(window.PriorityTB.Text);

            foreach (Agent agent in AgentListView.SelectedItems)
            {

                agent.Priority = Convert.ToInt32(window.PriorityTB.Text);
            }
            try
            {
                Tokarev_GlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdatePage();
        }
        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AgentListView.SelectedItems.Count > 1)
            {
                ChangePriorityButton.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriorityButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
