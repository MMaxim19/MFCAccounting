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
        MainWindow _mainWindow1;
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

        private void Print(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                TableGrid.Measure(pageSize);
                TableGrid.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(TableGrid, Title);
            }
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
    }
}
