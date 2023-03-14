using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        MFCEntities mfc;
        DataWinAadmin dwAdmin;
        EquipmentAccounting eA = new EquipmentAccounting();
        public EditWindow(int id, DataWinAadmin dataWinAadmin)
        {
            mfc = new MFCEntities();
            InitializeComponent();
            var eA = mfc.EquipmentAccounting.Where(x => x.ID == id).FirstOrDefault();
            DataContext = eA;
            EquipmentTable.ItemsSource = mfc.EquipmentModel.ToList();
            Status.ItemsSource = mfc.Status.ToList();
        }

        private void editButton(object sender, RoutedEventArgs e)
        {
            mfc.SaveChanges();
            TableGrid.ItemsSource = mfc.EquipmentAccounting.ToList();
            Close();
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
