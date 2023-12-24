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
        private Agent _currentAgent = new Agent();



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
                _currentAgent = SelectedAgent;
                ComboType.SelectedIndex = _currentAgent.AgentTypeID + 1;
            }

            DataContext = _currentAgent;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var context = Tokarev_GlazkiSaveEntities.GetContext();

            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
            {
                errors.AppendLine("Укажите название услуги");
            }
            else if (context.Agent.Any(agent => agent.Title == _currentAgent.Title && agent.ID != _currentAgent.ID))
            {
                errors.AppendLine("Уже существует такая услуга");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
            {
                errors.AppendLine("Укажите адрес услуги");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
            {
                errors.AppendLine("Укажите ФИО директора");
            }
            if (ComboType.SelectedItem == null)
            {
                errors.AppendLine("Укажите тип агента");
            }
            else
            {
                _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
            {
                errors.AppendLine("Укажите приоритет агента");
            }
            if (_currentAgent.Priority <= 0)
            {
                errors.AppendLine("Укажите положительный приоритет агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
            {
                errors.AppendLine("Укажите ИНН агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
            {
                errors.AppendLine("Укажите КПП агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
            {
                errors.AppendLine("Укажите телефон агента");
            }
            else
            {
                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(")", "").Replace(" ", "");
                if (ph.Length > 1)
                {
                    if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 10) || (ph[1] == '3' && ph.Length != 11))
                        errors.AppendLine("Укажите правильно телефон агента");
                }
                else if (ph[0] != 8 || ph[0] != 7) errors.AppendLine("Укажите правильно телефон агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
            {
                errors.AppendLine("Укажите почту агента");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentAgent.ID == 0)
                Tokarev_GlazkiSaveEntities.GetContext().Agent.Add(_currentAgent);

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
