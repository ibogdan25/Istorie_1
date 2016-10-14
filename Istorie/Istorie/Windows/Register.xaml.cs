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

namespace Istorie.Windows
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void connect_bttn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.Equals(parolaUtilizator.Password, repetareParolaUtilizator.Password))
            {
                MessageBox.Show("Parola nu sunt identice");
                return;
            }
            if (string.IsNullOrEmpty(numeUtilizator.Text) || string.IsNullOrWhiteSpace(numeUtilizator.Text) || string.IsNullOrEmpty(parolaUtilizator.Password) || string.IsNullOrWhiteSpace(parolaUtilizator.Password))
            {
                MessageBox.Show("Nu ai introdus numele de utilizator sau parola.");
                return;
            }
            if (dataNasterii.SelectedDate == null)
            {
                MessageBox.Show("Nu ati selectat data nasterii.");
                return;
            }
            if (dataNasterii.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Data nasterii este gresita.");
                return;
            }
            using(IstorieEntities context=new IstorieEntities())
            {
                try {
                    int rolul = (bool)elevRadio.IsChecked ? 1 : 2;
                    User user = new User()
                    {
                        name = numeUtilizator.Text,
                        fullName = numeUtilizator.Text,
                        pass = parolaUtilizator.Password,
                        email = emailUtilizator.Text,
                        birthday = dataNasterii.SelectedDate,
                        role = rolul
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MessageBox.Show("Utilizator adaugat");
            }

        }
    }
}
