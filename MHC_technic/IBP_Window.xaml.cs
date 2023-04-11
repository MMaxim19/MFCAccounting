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
    /// Логика взаимодействия для IBP_Window.xaml
    /// </summary>
    public partial class IBP_Window : Window
    {
        MFC_Entities MFC;
        public IBP_Window()
        {
            MFC= new MFC_Entities();
            InitializeComponent();
            TableIBP.ItemsSource = MFC.EquipmentModel.Where(x => x.EquipmentType == 1).ToList();
        }
    }
}
