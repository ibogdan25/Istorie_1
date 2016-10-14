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
            mesajDeIntampinare.Content = mesajDeIntampinare.Content + UserActual.user.fullName+"!";
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
            this.Show();
            
        }

        private void dataEveniment_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            using(IstorieEntities context=new IstorieEntities())
            {
                evenimente.Children.Clear();
                var query = (from x in context.Evenimentes
                            where x.data.Value.Month == dataEveniment.SelectedDate.Value.Month && x.data.Value.Day == dataEveniment.SelectedDate.Value.Day
                            select x).ToList();
                foreach(var eveniment in query)
                {
                    //Label
                    Label label = new Label();
                    label.Margin = new Thickness(0);
                    TextBlock text = new TextBlock();
                    text.Margin = new Thickness(0);
                    text.Padding = new Thickness(0);
                    text.Text = eveniment.data.Value.Year + " : " + eveniment.text;
                    text.TextWrapping = TextWrapping.Wrap;
                    label.Content = text;
                    evenimente.Children.Add(label);
                }
            }
        }
    }
}
