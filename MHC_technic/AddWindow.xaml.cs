﻿using System;
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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        MFC_Entities MFC;
        EquipmentAccounting Accounting;
        public AddWindow(MFC_Entities MFC)
        {
            InitializeComponent();
            this.MFC = MFC;
            Accounting = new EquipmentAccounting();
            this.DataContext = Accounting;
            this.Equipment.ItemsSource = MFC.EquipmentModel.ToList();
            this.Status.ItemsSource = MFC.Status.ToList();
            this.Status.MaxDropDownHeight= 0;
        }
        public AddWindow(MFC_Entities MFC, EquipmentAccounting accounting)
        {
            InitializeComponent();
            this.MFC = MFC;
            Accounting = new EquipmentAccounting();
            this.DataContext = accounting;
            this.Equipment.ItemsSource = MFC.EquipmentModel.ToList();
        }

        private void addButton(object sender, RoutedEventArgs e)
        {
            EquipmentAccounting ups = new EquipmentAccounting();
            var model = Equipment.SelectedItem as EquipmentModel;
            ups.Equipment = model.ID_ModelE;
            ups.SerialNumber = SerialNumber.Text;
            ups.InventoryNumber = InventoryNumber.Text;
            var upsStatus = Status.SelectedItem as Status;
            ups.EquipmentStatus = upsStatus.ID_status;
            ups.DeliveryDate = DateTime.Parse(Convert.ToString(DeliveryDate));

            MessageBox.Show("Запись успешно добавлена!");

            try
            {
                MFC.EquipmentAccounting.Add(ups);
                MFC.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Status.SelectedIndex= 0;
        }
    }
}
