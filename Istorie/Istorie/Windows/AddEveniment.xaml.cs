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
    /// Interaction logic for AddEveniment.xaml
    /// </summary>
    public partial class AddEveniment : Window
    {
        public AddEveniment()
        {
            InitializeComponent();
        }
        Evenimente evenimentInitial;
        public AddEveniment(Evenimente _eveniment)
        {
            InitializeComponent();
            evenimentInitial = _eveniment;
            textEveniment.AppendText(evenimentInitial.text);
            anEveniment.Text = evenimentInitial.data.Value.Year.ToString();
            isDHr.IsChecked = evenimentInitial.isDHr;
            if (!(bool)isDHr.IsChecked) isIHr.IsChecked = true;
            evenimentData.SelectedDate = evenimentInitial.data;
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (evenimentInitial == null)
            {
                using (IstorieEntities context = new IstorieEntities())
                {
                    try
                    {
                        Evenimente eveniment = new Evenimente();
                        DateTime evenimentDateTime = (DateTime)(evenimentData.SelectedDate == null ? DateTime.Now : evenimentData.SelectedDate);
                        int an = 0;
                        int an_1 = evenimentDateTime.Year;
                        try
                        {
                            an = int.Parse(anEveniment.Text.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Anul nu are formatul corespunzator.");
                            return;
                        }
                        evenimentDateTime = evenimentDateTime.AddYears(an);
                        evenimentDateTime = evenimentDateTime.AddYears(-an_1);
                        eveniment.data = evenimentDateTime;
                        TextRange text = new TextRange(textEveniment.Document.ContentStart, textEveniment.Document.ContentEnd);
                        if (text.Text.Length < 10)
                        {
                            MessageBox.Show("Lungimea textului trebuie sa fie mai mare sau egala cu 10");
                            return;
                        }
                        eveniment.text = text.Text;
                        eveniment.isDHr = (bool)isDHr.IsChecked;
                        context.Evenimentes.Add(eveniment);
                        context.SaveChanges();
                        MessageBox.Show("Eveniment adaugat cu succes.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }
            else
            {
                using (IstorieEntities context=new IstorieEntities())
                {
                    try {
                        DateTime evenimentDateTime = (DateTime)(evenimentData.SelectedDate == null ? DateTime.Now : evenimentData.SelectedDate);
                        int an = 0;
                        int an_1 = evenimentDateTime.Year;
                        try
                        {
                            an = int.Parse(anEveniment.Text.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Anul nu are formatul corespunzator.");
                            return;
                        }
                        TextRange text = new TextRange(textEveniment.Document.ContentStart, textEveniment.Document.ContentEnd);
                        if (text.Text.Length < 10)
                        {
                            MessageBox.Show("Lungimea textului trebuie sa fie mai mare sau egala cu 10");
                            return;
                        }
                        var entry = context.Entry(evenimentInitial);
                        if (entry.State == System.Data.Entity.EntityState.Detached)
                        {
                            context.Evenimentes.Attach(evenimentInitial);
                        }
                        evenimentInitial.text = text.Text;
                        evenimentInitial.isDHr = isDHr.IsChecked;
                        evenimentInitial.data = evenimentData.SelectedDate;
                        context.SaveChanges();
                        MessageBox.Show("Eveniment modificat cu succes.");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void renunta_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
