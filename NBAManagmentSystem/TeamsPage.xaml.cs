using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для TeamsPage.xaml
    /// </summary>
    public partial class TeamsPage : Page
    {
        List<TextBlock> tbLV = new List<TextBlock>();
        public TeamsPage()
        {
            InitializeComponent();
            LVConference.SelectedIndex = 0;
            LVConference.ItemsSource = dbcl.dbP.Conference.ToList();
        }

        private void TBConfereceMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            LVDivision.ItemsSource = dbcl.dbP.Division.AsEnumerable().Where(x => x.ConferenceId == Convert.ToInt32(tb.Uid));

            foreach (TextBlock t in tbLV)
            {
                t.Background = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
            }
            tb.Background = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
        }

        private void DivisionNameLoaded(object sender, RoutedEventArgs e)
        {
            LVDivision.ItemsSource = dbcl.dbP.Division.AsEnumerable().Where(x => x.ConferenceId == LVConference.SelectedIndex + 1);
        }

        private void LVTeamLoaded(object sender, RoutedEventArgs e)
        {
            ListView lv = sender as ListView;
            lv.ItemsSource = dbcl.dbP.Team.AsEnumerable().Where(x => x.DivisionId == Convert.ToInt32(lv.Uid));
        }

        private void RosterMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            ClFrame.AddPage(++ClFrame.NumThis, new PlayersPage(dbcl.dbP.Team.AsEnumerable().First(x => x.TeamId == Convert.ToInt32(tb.Uid)).TeamName, false));
        }

        private void MatchupMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            ClFrame.AddPage(++ClFrame.NumThis, new MatchupPage(Convert.ToInt32(tb.Uid)));
        }

        private void FirstLineupMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            ClFrame.AddPage(++ClFrame.NumThis, new PlayersPage(dbcl.dbP.Team.AsEnumerable().First(x => x.TeamId == Convert.ToInt32(tb.Uid)).TeamName, true));
        }

        private void TBConferenceLoaded(object sender, RoutedEventArgs e)
        {

            tbLV.Add(sender as TextBlock);
            tbLV[0].Background = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
        }
    }
}
