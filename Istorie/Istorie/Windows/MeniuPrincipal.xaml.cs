using Istorie.Utils;
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
    /// Interaction logic for MeniuPrincipal.xaml
    /// </summary>
    public partial class MeniuPrincipal : Window
    {
        public MeniuPrincipal()
        {
            InitializeComponent();
            dataEveniment.SelectedDate = DateTime.Now;
            incarcaEvenimente();
            mesajDeIntampinare.Content = mesajDeIntampinare.Content + "" + UserActual.user.fullName + "!";
            if (UserActual.user.role != (int)User.rol.Admin)
            {
                meniu.Children.Remove(addEveniment);
                meniu.Children.Remove(modifEveniment);
            }
        }

        private void addEveniment_Click(object sender, RoutedEventArgs e)
        {
            AddEveniment form = new AddEveniment();
            this.Hide();
            form.ShowDialog();
            incarcaEvenimente();
            this.Show();
            
        }

        private void dataEveniment_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            incarcaEvenimente();
        }
        public void incarcaEvenimente()
        {
            using (IstorieEntities context = new IstorieEntities())
            {
                evenimentePanel.Children.Clear();
                var query = (from x in context.Evenimentes
                             where x.data.Value.Month == dataEveniment.SelectedDate.Value.Month && x.data.Value.Day == dataEveniment.SelectedDate.Value.Day
                             select x).ToList();
                foreach (var eveniment in query)
                {
                    //TextBlock
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = eveniment.data.Value.Year + " : " + eveniment.text;
                    textBlock.TextWrapping = TextWrapping.Wrap;
                    evenimentePanel.Children.Add(textBlock);
                }
            }
        }

        private void modifEveniment_Click(object sender, RoutedEventArgs e)
        {
            ModifEveniment form = new ModifEveniment();
            this.Hide();
            form.ShowDialog();
            incarcaEvenimente();
            this.ShowDialog();
        }
    }
}
