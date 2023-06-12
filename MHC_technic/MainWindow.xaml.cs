using MFC_technic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MHC_technic
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MFC_Entities MFC;
        public MainWindow()
        {
            InitializeComponent();
            MFC = new MFC_Entities();
        }

        private void enterButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var userAuth = MFC.User.FirstOrDefault(x => x.Login == Login.Text && x.Password == Password.Password);
                if (userAuth == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка авторизации!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (userAuth.UserRole)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать, администратор", "Успешно!");
                            DataWinAadmin windowAdmin = new DataWinAadmin();
                            windowAdmin.Show();
                            Close();
                            break;
                        case 2:
                            MessageBox.Show("Добро пожаловать, пользователь", "Успешно!");
                            DataWinUser windowUser1 = new DataWinUser();
                            windowUser1.Show();
                            Close();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString());
            }
        }

        private void exitButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
