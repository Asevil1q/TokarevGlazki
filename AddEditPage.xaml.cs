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
using Microsoft.Win32;

namespace TokarevGlazki
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent currentAgent = new Agent();



        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;

            var currentClientServices = Tokarev_GlazkiSaveEntities.GetContext().Agent.ToList();
            currentClientServices = currentClientServices.Where(p => p.AgentTypeID == currentAgent.ID).ToList();

            if (currentClientServices.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существуют записи на эту услугу");
            else
            {

                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Tokarev_GlazkiSaveEntities.GetContext().Agent.Remove(currentAgent);
                        Tokarev_GlazkiSaveEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                currentAgent = SelectedAgent;
                ComboType.SelectedIndex = currentAgent.AgentTypeID + 1;
            }

            DataContext = currentAgent;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
            {
                errors.AppendLine("Укажите наименование агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Address))
            {
                errors.AppendLine("Укажите адрес агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName))
            {
                errors.AppendLine("Укажите ФИО директора");
            }
            if (ComboType.SelectedItem == null)
            {
                errors.AppendLine("Укажите тип агента");
            }
            else
            {
                currentAgent.AgentTypeID = ComboType.SelectedIndex - 1;
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString()))
            {
                errors.AppendLine("Укажите приоритет");
            }
            if (currentAgent.Priority <= 0)
            {
                errors.AppendLine("Укажите положительный приоритет агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
            {
                errors.AppendLine("Укажите инн агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.KPP))
            {
                errors.AppendLine("Укажите КПП агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
            {
                errors.AppendLine("Укажите телефон агкента");
            }
            else
            {
                string ph = currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11)
                    || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Email))
                errors.AppendLine("Укажите почту агента");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (currentAgent.ID == 0)
                Tokarev_GlazkiSaveEntities.GetContext().Agent.Add(currentAgent);

            try
            {
                Tokarev_GlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
   

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myopenFileDialog = new OpenFileDialog();
            if (myopenFileDialog.ShowDialog() == true)
            {
                LogoImage.Source = new BitmapImage(new Uri(myopenFileDialog.FileName));
            }
        }

 

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SalesHistory((sender as Button).DataContext as Agent));
        }
    }
}
