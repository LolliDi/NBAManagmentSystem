using System.Windows;
using System.Windows.Controls;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для VisitorMenu.xaml
    /// </summary>
    public partial class VisitorMenu : Page
    {
        public VisitorMenu()
        {
            InitializeComponent();
        }

        private void PhotosClick(object sender, RoutedEventArgs e)
        {
            ClFrame.AddPage(++ClFrame.NumThis, new PhotoPage());
        }

        private void MatchupsClick(object sender, RoutedEventArgs e)
        {
            ClFrame.AddPage(++ClFrame.NumThis, new MatchupPage());
        }

        private void TeamsClick(object sender, RoutedEventArgs e)
        {
            ClFrame.AddPage(++ClFrame.NumThis, new TeamsPage());
        }

        private void PlayersClick(object sender, RoutedEventArgs e)
        {
            ClFrame.AddPage(++ClFrame.NumThis, new PlayersPage());
        }
    }
}
