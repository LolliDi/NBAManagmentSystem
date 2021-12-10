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

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для MatchupPage.xaml
    /// </summary>
    public partial class MatchupPage : Page
    {   
        public MatchupPage()
        {
            InitializeComponent();
            DatePickerSort.SelectedDate = new DateTime(2017, 1, 28);
        }

        private void DatePickerSortSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //for (int i = 0; i < LVMatchupList.Items.Count;i++)
            {
                LVMatchupList.Items.Clear();
            }
            DateTime dt = DatePickerSort.SelectedDate.Value;
            foreach (Matchup m in dbcl.dbP.Matchup.ToList())
            {
                if(m.Starttime.Day== dt.Day&& m.Starttime.Month == dt.Month&& m.Starttime.Year == dt.Year)
                {
                    LVMatchupList.Items.Add(m);
                }
            }
            if(LVMatchupList.Items.Count>0)
            {
                TBNoMatchups.Visibility = Visibility.Collapsed;
            }
            else
            {
                TBNoMatchups.Visibility = Visibility.Visible;
            }
        }

        private void ButtonBackDateClick(object sender, RoutedEventArgs e)
        {
            DatePickerSort.SelectedDate = DatePickerSort.SelectedDate.Value.AddDays(-1);
        }
        private void ButtonNextDateClick(object sender, RoutedEventArgs e)
        {
            DatePickerSort.SelectedDate = DatePickerSort.SelectedDate.Value.AddDays(1);
        }
        private void ViewClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int id = Convert.ToInt32(b.Uid);
            LVpr.ItemsSource = dbcl.dbP.Matchup.Where(x => x.MatchupId == id).ToList();
        }
    }
}
