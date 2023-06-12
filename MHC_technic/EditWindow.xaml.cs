using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        EquipmentAccounting EquipmentAccounting;
        DataWinAadmin _dataWinAadmin1;
        MFC_Entities Mfc;
        public EditWindow(EquipmentAccounting equipmentAccounting, DataWinAadmin dataWinAadmin)
        {
            InitializeComponent();
            EquipmentAccounting = equipmentAccounting;
            _dataWinAadmin1= dataWinAadmin;
            Mfc = _dataWinAadmin1.mfc;
            DataContext = equipmentAccounting;
        }
        private void Input()
        {
            if (EquipmentCB.SelectedIndex == -1)
            {
                EquipmentAccounting.Equipment = null;
            }
            else
            {
                EquipmentAccounting.Equipment = EquipmentCB.SelectedIndex +1;
            }
            if (StatusCB.SelectedIndex == -1)
            {
                EquipmentAccounting.EquipmentStatus = null;
            }
            else
            {
                EquipmentAccounting.EquipmentStatus = StatusCB.SelectedIndex +1;
            }
            EquipmentAccounting.SerialNumber = String.IsNullOrEmpty(SerialNumber.Text) ? string.Empty : ((SerialNumber.Text, @"\d+").Text);
            EquipmentAccounting.InventoryNumber = String.IsNullOrEmpty(InventoryNumber.Text) ? string.Empty : ((InventoryNumber.Text, @"\d+").Text);
            EquipmentAccounting.DeliveryDate = String.IsNullOrEmpty(DatePic.Text) ? DateTime.Now : DateTime.Parse(DatePic.Text);
        } 

        private void editButton(object sender, RoutedEventArgs e)
        {
            Input();
            Mfc.SaveChanges();
            _dataWinAadmin1.ReadData();
            Close();
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EquipmentCB.ItemsSource = Mfc.EquipmentModel.ToList();
            EquipmentCB.SelectedIndex = EquipmentAccounting.Equipment is null ? -1 : (int)EquipmentAccounting.Equipment -1;

            StatusCB.ItemsSource = Mfc.Status.ToList();
            StatusCB.SelectedIndex = EquipmentAccounting.EquipmentStatus is null ? -1 : (int)EquipmentAccounting.EquipmentStatus -1;
        }
    }
}
