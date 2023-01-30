using NotePadCalendar.Model;
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

namespace NotePadCalendar.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LvNote.ItemsSource = App.DB.Note.Where(x => x.UserId == App.LoggedUser.Id).ToList();
            Filtr.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
        
                string errorMessage = "";

                if (DpDate.SelectedDate == null)
                {
                    errorMessage += "Выберите дату\n";
                }
                if (string.IsNullOrWhiteSpace(TbDescription.Text))
                {
                    errorMessage += "Введите описание\n";
                }
            if (DpDate.SelectedDate < DateTime.Now)
            {
                errorMessage += "Выберете дату не из прошлого\n";
            }
            if (string.IsNullOrWhiteSpace(errorMessage) == false)
                {
                    MessageBox.Show(errorMessage);
                    return;
                }
            if (MessageBox.Show("Вы действительно хотит добавить новую запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.DB.Note.Add(new Note()
                {
                    NoteDate = DpDate.Text,
                    Description = TbDescription.Text,
                    UserId = App.LoggedUser.Id,

                });
                App.DB.SaveChanges();
                TbDescription.Clear();
                DpDate.Text = "";
                Refresh();
            }
        }
     
        public void Refresh()
        {
           
            var product = App.DB.Note.Where(x => x.UserId == App.LoggedUser.Id).ToList().ToList();

            if (Filtr.SelectedIndex == 0)
            {
                var a = Filtr.SelectedIndex.ToString();
                product = App.DB.Note.Where(x => x.UserId == App.LoggedUser.Id).ToList().ToList();
            }
            if (Filtr.SelectedIndex == 1)
            {
                product = product.OrderBy(x => x.NoteDate).ToList();
            }
            if (Filtr.SelectedIndex == 2)
            {
                product = product.OrderByDescending(x => x.NoteDate).ToList();
            }
            LvNote.ItemsSource = product.ToList();
        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотит выйти из учетной запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.LoggedUser = null;
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}
