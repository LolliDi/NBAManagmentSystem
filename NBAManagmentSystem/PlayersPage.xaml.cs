using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для PlayersPage.xaml
    /// </summary>
    /// 

    public partial class PlayersPage : Page
    {
        int _AlphId = 0;
        List<string> _a = new List<string>();
        PageChange pc = new PageChange();
        List<Player> _filt = new List<Player>();
        public PlayersPage()
        {
            InitializeComponent();
            initialPlayers();
            ComBTeam.SelectedIndex = 0;
            ComBSeason.SelectedIndex = 0;

        }

        public PlayersPage(string nameTeam, bool firstRoster)
        {
            InitializeComponent();
            initialPlayers();
            ComBTeam.SelectedItem = nameTeam;
            if (firstRoster)
            {
                ComBSeason.SelectedItem = "2013-2014";
            }
            else
            {
                ComBSeason.SelectedIndex = 0;
            }

        }

        private void initialPlayers()
        {
            _a.Add("All");
            int i = 1;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                _a.Add("" + c);
                i++;
            }
            LVAlphabet.ItemsSource = _a.ToList();
            ComBSeason.Items.Add("All");
            foreach (Season s in dbcl.dbP.Season.ToList())
            {
                ComBSeason.Items.Add(s.Name);
            }

            ComBTeam.Items.Add("All");
            foreach (Team t in dbcl.dbP.Team.ToList())
            {
                ComBTeam.Items.Add(t.TeamName);
            }

            _filt = dbcl.dbP.Player.ToList();
            pc.CountItemsInListGetSet = _filt.Count();
            GoToPage(1);
        }
        private void AlphLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Text = _a[_AlphId];
            tb.Uid = _a[_AlphId];
            _AlphId++;
        }

        private void ImagePlayerLoaded(object sender, RoutedEventArgs e)
        {
            Image image = sender as Image;
            int id = Convert.ToInt32(image.Uid);
            byte[] Img = dbcl.dbP.Player.FirstOrDefault(x => x.PlayerId == id).Img;
            if (Img != null)
            {
                BitmapImage Photo = new BitmapImage();
                byte[] buff = Img;
                using (MemoryStream MS = new MemoryStream(buff))
                {
                    Photo.BeginInit();
                    Photo.StreamSource = MS;
                    Photo.CacheOption = BitmapCacheOption.OnLoad;
                    Photo.EndInit();
                }
                image.Source = Photo;
            }
        }

        private bool GoToPage(int numPage)
        {
            if (numPage > 0 && numPage <= pc.CountPagesGetSet)
            {
                LVPlayers.ItemsSource = _filt.Skip((numPage - 1) * 4).Take(4);
                pc.thisPageNum = numPage;
                TBGoToPage.Text = "" + numPage;
                return true;
            }
            else
                return false;
        }

        string oldTB = "1";
        private void TBGoToPageTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TBGoToPage.Text == "0")
                {
                    oldTB = "1";
                    throw new Exception("хиихихи фиг тебе");
                }
                if(!GoToPage(Convert.ToInt32(TBGoToPage.Text)))
                {
                    oldTB = "" + pc.CountPagesGetSet;
                    throw new Exception("хиихихи фиг тебе");
                }
                oldTB = TBGoToPage.Text;
            }
            catch
            {
                if (TBGoToPage.Text != "")
                {
                    TBGoToPage.Text = oldTB;
                    TBGoToPage.CaretIndex = oldTB.Length;
                }
            }
        }

        private void GoToStartClick(object sender, RoutedEventArgs e)
        {
            GoToPage(1);
        }

        private void GoToPageBackClick(object sender, RoutedEventArgs e)
        {
            GoToPage(pc.thisPageNum - 1);
        }

        private void GoToPageNextClick(object sender, RoutedEventArgs e)
        {
            GoToPage(pc.thisPageNum + 1);
        }

        private void GoToPageEndClick(object sender, RoutedEventArgs e)
        {
            GoToPage(pc.CountPagesGetSet);
        }

        private void Filtr()
        {
            _filt.Clear();
            _filt = dbcl.dbP.Player.ToList();
            if (LVAlphabet.SelectedItem != "All")
            {
                string alp = LVAlphabet.SelectedItem as string;
                Regex r = new Regex(@"^" + LVAlphabet.SelectedItem + "\\w*");
                _filt = _filt.AsEnumerable().Where(x => r.IsMatch(x.Name)).ToList();
            }
            if (TBName.Text != "")
            {
                string name = TBName.Text.ToLower();
                _filt = _filt.AsEnumerable().Where(x => x.Name.ToLower().Contains(name)).ToList();
            }

            if (ComBSeason.SelectedItem != "All" && ComBSeason.SelectedItem != null)
            {
                string selInd = ComBSeason.SelectedItem as string;
                List<PlayerInTeam> pIT = dbcl.dbP.PlayerInTeam.ToList();
                _filt = _filt.Where(a => pIT.FirstOrDefault(b => b.PlayerId == a.PlayerId && b.Season.Name == selInd) != null).ToList();
            }

            if (ComBTeam.SelectedItem != "All" && ComBTeam.SelectedItem != null)
            {

                string nameT = ComBTeam.SelectedItem as string;
                List<PlayerInTeam> pIT = dbcl.dbP.PlayerInTeam.ToList();
                _filt = _filt.Where(x => pIT.FirstOrDefault(y => y.PlayerId == x.PlayerId && y.Team.TeamName == nameT) != null).ToList();
            }

            if (_filt.Count == 0)
            {
                LVPlayers.ItemsSource = null;
                TBNoResult.Visibility = Visibility.Visible;
            }
            else
            {
                TBNoResult.Visibility = Visibility.Collapsed;
            }
            LVPlayers.Items.Refresh();
            pc.CountItemsInListGetSet = _filt.Count();
            GoToPage(1);
            TBCountPage.Text = "" + pc.CountPagesGetSet;
        }

        private void LVPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void ComBSeasonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void ComBTeamSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void TBNameTextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }
    }
}
