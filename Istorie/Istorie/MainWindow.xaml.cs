﻿using Istorie.Utils;
using Istorie.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Configuration;
namespace Istorie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            SystemDeOperare.GetOS();
            //MessageBox.Show(Environment.OSVersion.ToString());
        }

        private void connect_bttn_Click(object sender, RoutedEventArgs e)
        {
            using(IstorieEntities context=new IstorieEntities())
            {
                var user = (from c in context.Users
                             where c.name == numeUtilizator.Text && c.pass == parolaUtilizator.Password
                             select c).FirstOrDefault();
                if (user!=null)
                {
                    UserActual.user = (User)user;
                    MeniuPrincipal form = new MeniuPrincipal();
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }else
                {
                    MessageBox.Show("Nume de utilizator sau parola gresita.");
                }
            }
            
        }

        private void registerLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Register form = new Register();
            Hide();
            form.ShowDialog();
            this.Close();
        }
    }
}
