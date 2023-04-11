using MHC_technic;
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
    /// Логика взаимодействия для DataWinUser.xaml
    /// </summary>
    public partial class DataWinUser : Window
    {
        MFC_Entities mfc;
        public DataWinUser()
        {
            mfc = new MFC_Entities();
            InitializeComponent();
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void UPSTable(object sender, RoutedEventArgs e)
        {
            IBP_Window IBP = new IBP_Window();
            IBP.Show();
        }

        private void ServerTable(object sender, RoutedEventArgs e)
        {
            ServerWindow Server = new ServerWindow();
            Server.Show();
        }

        private void MonitorTable(object sender, RoutedEventArgs e)
        {
            MonitorWindow Monitor = new MonitorWindow();
            Monitor.Show();
        }

        private void PrinterTable(object sender, RoutedEventArgs e)
        {
            PrinterWindow Printer = new PrinterWindow();
            Printer.Show();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            if (!(string.IsNullOrEmpty(input)))
            {
                int resultCount = mfc.EquipmentAccounting.Count(x => x.EquipmentModel.EquipmentName.Contains(input));
                TableGrid.ItemsSource = mfc.EquipmentAccounting.Where(x => x.EquipmentModel.EquipmentName.Contains(input)).ToList();
            }
            else
            {
                TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
            }
        }
    }
}
