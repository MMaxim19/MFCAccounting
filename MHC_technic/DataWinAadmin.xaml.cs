﻿using MHC_technic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Excel = Microsoft.Office.Interop.Excel;

namespace MFC_technic
{
    /// <summary>
    /// Логика взаимодействия для DataWinAadmin.xaml
    /// </summary>
    public partial class DataWinAadmin : Window
    {
        public FlowDocument flowDoc;
        public MFC_Entities mfc { get; set; }
        public List<EquipmentAccounting> equipmentAccounting;
        public DataWinAadmin()
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
            var line = (sender as Button).DataContext as EquipmentAccounting;
            EditWindow edit = new EditWindow(line, this);
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

        private void breakDown_Unchecked(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
        }
        private void inWork_Unchecked(object sender, RoutedEventArgs e)
        {
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
        }
        private void onRepair_Unchecked(object sender, RoutedEventArgs e)
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
        public void ReadData()
        {
            mfc = new MFC_Entities();
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            TableGrid.Columns[5].Visibility = Visibility.Hidden;
            TableGrid.Columns[6].Visibility = Visibility.Hidden;
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                TableGrid.Measure(pageSize);
                TableGrid.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(TableGrid, Title);
            }
            TableGrid.Columns[5].Visibility = Visibility.Visible;
            TableGrid.Columns[6].Visibility = Visibility.Visible;
        }

        public DataGrid GetTableGrid()
        {
            return TableGrid;
        }

        private void ExportExcel(object sender, RoutedEventArgs e)
        {
            GetTableGrid();
            TableGrid.SelectAllCells();
            TableGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, TableGrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            TableGrid.UnselectAllCells();
            StreamWriter file = new StreamWriter(@"C:\Users\Максим\Desktop\test1.xls", true, Encoding.GetEncoding(1251));
            file.WriteLine(result.Replace(',', ' '));
            file.Close();
            MessageBox.Show("Данные экспортированы в Excel!", "Успешно!");
            System.Diagnostics.Process.Start(@"C:\Users\Максим\Desktop\test1.xls");
        }
    }
}
