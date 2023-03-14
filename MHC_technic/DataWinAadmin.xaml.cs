using MHC_technic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для DataWinAadmin.xaml
    /// </summary>
    public partial class DataWinAadmin : Window
    {
        MFCEntities mfc;
        public DataWinAadmin()
        {
            mfc = new MFCEntities();
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

        private void ServerTable(object sender, RoutedEventArgs e)
        {
            ServerWindow Server = new ServerWindow();
            Server.Show();
        }

        private void UserTable(object sender, RoutedEventArgs e)
        {
            WorkerWin worker = new WorkerWin();
            worker.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //var Dob = new EquipmentAccounting();
            //mfc.EquipmentAccounting.Add(Dob);
            var Dob1 = new AddWindow(mfc);
            Dob1.ShowDialog();
            Update();
        }
        private void Update()
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
            TableGrid.Items.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            mfc = new MFCEntities();
            EquipmentAccounting eAccounting = TableGrid.SelectedItem as EquipmentAccounting;
            EquipmentAccounting eAccounting1 = mfc.EquipmentAccounting.Where(x => x.ID == eAccounting.ID).Single();
            EditWindow edit = new EditWindow(eAccounting1.ID, this);
            edit.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var dea = TableGrid.SelectedItem as EquipmentAccounting;
            if (dea == null)
            {
                MessageBox.Show("Выберите строку");
                return;
            }
            mfc.EquipmentAccounting.Remove(dea);
            MessageBox.Show("Удалено");
            mfc.SaveChanges();
            Update();
        }

        private void inWork_Checked(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.Where(x => x.EquipmentStatus == 1).ToList();
        }

        private void onRepair_Checked(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.Where(x => x.EquipmentStatus == 2).ToList();
        }

        private void breakDown_Checked(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.Where(x => x.EquipmentStatus == 3).ToList();
        }

        private void clearFilter_Checked(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
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

        private void RefreshTable(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
        }
    }
}
