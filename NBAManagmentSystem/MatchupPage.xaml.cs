using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для MatchupPage.xaml
    /// </summary>
    public partial class MatchupPage : Page
    {
        List<Matchup> _filt = new List<Matchup>();
        public MatchupPage()
        {
            InitializeComponent();
            _filt = dbcl.dbP.Matchup.ToList();
            DatePickerSort.SelectedDate = new DateTime(2017, 1, 28);
        }

        public MatchupPage(int idTeam)
        {
            InitializeComponent();
            _filt = dbcl.dbP.Matchup.Where(x => x.Team_Home == idTeam || x.Team_Away == idTeam).ToList();
            DatePickerSort.SelectedDate = new DateTime(2017, 1, 28);
            foreach (Matchup m in _filt)
            {
                LVMatchupList.Items.Add(m);
            }
            if (LVMatchupList.Items.Count > 0)
            {
                TBNoMatchups.Visibility = Visibility.Collapsed;
            }
            else
            {
                TBNoMatchups.Visibility = Visibility.Visible;
            }
        }

        private void DatePickerSortSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LVMatchupList.Items.Clear();
            DateTime dt = DatePickerSort.SelectedDate.Value;
            foreach (Matchup m in _filt)
            {
                if (m.Starttime.Day == dt.Day && m.Starttime.Month == dt.Month && m.Starttime.Year == dt.Year)
                {
                    LVMatchupList.Items.Add(m);
                }
            }
            if (LVMatchupList.Items.Count > 0)
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
