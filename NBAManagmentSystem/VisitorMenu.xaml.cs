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
