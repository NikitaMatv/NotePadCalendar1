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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        private void BtnRegFinish_Click(object sender, RoutedEventArgs e)
        {

            if (TbLogin.Text.Trim().Length <= 0 || PbPassword.Password.Trim().Length <= 0 || PbTwoPassword.Password.Trim().Length <= 0)
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (PbPassword.Password.Trim() != PbTwoPassword.Password.Trim())
                {
                    MessageBox.Show("Пароли не совпадают!");
                }
                else
                {
                    App.LoggedUser = App.DB.User.ToList().Find(x => x.Login == TbLogin.Text);
                    if (App.LoggedUser != null)
                    {
                        MessageBox.Show("Такой логин уже есть!");
                    }
                    else
                    {
                        if (MessageBox.Show("Вы действительно хотите зарегестрировать учетную запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            App.DB.User.Add(new User()
                            {
                                Login = TbLogin.Text.Trim(),
                                Password = PbPassword.Password.Trim(),
                            });
                            App.DB.SaveChanges();
                            NavigationService.Navigate(new LoginPage());
                        }
                    }

                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить регестрацию?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}
