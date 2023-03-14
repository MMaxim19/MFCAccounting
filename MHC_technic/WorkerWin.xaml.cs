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
using System.Windows.Shapes;

namespace MFC_technic
{
    /// <summary>
    /// Логика взаимодействия для WorkerWin.xaml
    /// </summary>
    public partial class WorkerWin : Window
    {
        MFCEntities mfc;
        public WorkerWin()
        {
            mfc= new MFCEntities();
            InitializeComponent();
            Worker.ItemsSource = mfc.User.ToList();
        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            DataWinAadmin winAadmin= new DataWinAadmin();
            winAadmin.Show();
            Close();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            if(!(string.IsNullOrEmpty(input))) 
            {
                int result = mfc.User.Count(x => x.Role.RoleName.Contains(input));
                Worker.ItemsSource = mfc.User.Where(x => x.Role.RoleName.Contains(input)).ToList();
            }
            else
            {
                Worker.ItemsSource = mfc.User.ToList();
            }
        }
    }
}
