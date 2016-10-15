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
    /// Interaction logic for ModifEveniment.xaml
    /// </summary>
    public partial class ModifEveniment : Window
    {
        public ModifEveniment()
        {
            InitializeComponent();
            evenimentData.SelectedDate = DateTime.Now;
            incarca();
        }

        private void evenimentData_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            incarca();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Sigur doriti sa stergeti evenimentul?", "Atentie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (IstorieEntities context=new IstorieEntities())
                {
                    Button deleteButton = (Button)sender;
                    var entry = context.Entry((Evenimente)deleteButton.Tag);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                    {
                        context.Evenimentes.Attach((Evenimente)deleteButton.Tag);
                    }
                    context.Evenimentes.Remove((Evenimente)deleteButton.Tag);
                    context.SaveChanges();
                    MessageBox.Show("Operatiunea s-a realizat cu succes");
                    incarca();
                }
            }
        }
        private void modifButton_Click(object sender, RoutedEventArgs e)
        {

        }
        public void incarca()
        {
            using (IstorieEntities context=new IstorieEntities())
            {
                evenimentePanel.Children.Clear();
                var query = (from x in context.Evenimentes
                             where x.data.Value.Month == evenimentData.SelectedDate.Value.Month && x.data.Value.Day == evenimentData.SelectedDate.Value.Day
                             select x).ToList();
                foreach (var eveniment in query)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = new SolidColorBrush(Color.FromRgb(0,0,0));
                    border.Margin = new Thickness(2);

                    //TextBloc
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text =eveniment.data.Value.Year+" : "+ eveniment.text;
                    textBlock.TextWrapping = TextWrapping.Wrap;
                    textBlock.Margin = new Thickness(2);

                    //StackPanel All
                    StackPanel stackPanelAll = new StackPanel();
                    stackPanelAll.Children.Add(textBlock);

                    //StackPanel
                    StackPanel stack = new StackPanel();
                    stack.Orientation = Orientation.Horizontal;

                    //modifButton
                    Button modifButton = new Button();
                    modifButton.Width = 80;
                    modifButton.Height = 27;
                    modifButton.Content = "Modifica";
                    modifButton.Tag = eveniment;
                    modifButton.Margin = new Thickness(2, 0, 10, 2);
                    stack.Children.Add(modifButton);

                    //deleteButton
                    Button deleteButton = new Button();
                    deleteButton.Width = 80;
                    deleteButton.Height = 27;
                    deleteButton.Content = "Sterge";
                    deleteButton.Tag = eveniment;
                    deleteButton.Margin = new Thickness(2, 0, 0, 2);
                    deleteButton.Click += new RoutedEventHandler(deleteButton_Click);
                    stack.Children.Add(deleteButton);

                    stackPanelAll.Children.Add(stack);
                    border.Child = stackPanelAll;

                    evenimentePanel.Children.Add(border);
                   
                }
            }
        }
    }
}
