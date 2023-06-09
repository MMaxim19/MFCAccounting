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
    /// Логика взаимодействия для ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {
        MFC_Entities MFC;
        public ServerWindow()
        {
            MFC = new MFC_Entities();
            InitializeComponent();
            TableMonitor.ItemsSource = MFC.EquipmentModel.Where(x => x.EquipmentType == 4).ToList();
        }
    }
}
