using NotePadCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Логика взаимодействия для PasswordEdd.xaml
    /// </summary>
    public partial class PasswordEdd : Page
    {
        User contextuser;
        public PasswordEdd(User user)
        {
            InitializeComponent();
           contextuser= user;
            DataContext= contextuser;
            TbLogin.Text = contextuser.Login;
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
                     
                      
                            if (MessageBox.Show($"Вы действительно хотите изменить пароль учетной записи {contextuser.Login}?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                            contextuser.Password = PbPassword.Password.Trim();
                        if(contextuser.Id == 0)
                        {
                            App.DB.User.Add(contextuser);
                        } 
                           
                               
                                App.DB.SaveChanges();
                                NavigationService.Navigate(new LoginPage());
                            }
                        

                    }
                }
            }
        

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить регестрацию?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new LoginPage());
            }
        }
    }
}
