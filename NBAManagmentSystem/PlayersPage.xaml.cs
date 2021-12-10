using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PlayersPage.xaml
    /// </summary>
    public partial class PlayersPage : Page
    {
        int _AlphId = 0;
        List<string> _a = new List<string>();
        PageChange pc = new PageChange();
        List<Player> _filt = new List<Player>();
        public PlayersPage()
        {
            InitializeComponent();
           
            _a.Add("All");
            int i = 1;
            for(char c='A';c<='Z';c++)
            {
                _a.Add("" +c);
                i++;
            }
            LVAlphabet.ItemsSource = _a.ToList();
            ComBSeason.Items.Add("All");
            foreach(Season s in dbcl.dbP.Season.ToList())
            {
                ComBSeason.Items.Add(s.Name);
            }

            ComBTeam.Items.Add("All"); 
            foreach (Team t in dbcl.dbP.Team.ToList())
            {
                ComBTeam.Items.Add(t.TeamName);
            }
            ComBTeam.SelectedIndex = 0;
            ComBSeason.SelectedIndex = 0;
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

        private void GoToPage(int numPage)
        {
            if(numPage>0&&numPage<=pc.CountPagesGetSet)
            {
                LVPlayers.ItemsSource = _filt.Skip((numPage-1)*4).Take(4);
                pc.thisPageNum = numPage;
                TBGoToPage.Text = "" + numPage;
            }
        }

        private void TBGoToPageTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                GoToPage(Convert.ToInt32(TBGoToPage.Text));
            }
            catch
            {

            }
        }

        private void GoToStartClick(object sender, RoutedEventArgs e)
        {
            GoToPage(1);
        }

        private void GoToPageBackClick(object sender, RoutedEventArgs e)
        {
            GoToPage(pc.thisPageNum-1);
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
            if(LVAlphabet.SelectedItem == "All")
            {
                _filt = dbcl.dbP.Player.ToList();
            }
            else
            {
                Regex r = new Regex(@"^"+ LVAlphabet.SelectedItem + "\\w*");
                foreach(Player p in dbcl.dbP.Player.ToList())
                {
                    if(r.IsMatch(p.Name))
                    {
                        _filt.Add(p);
                    }
                }
            }
            if(TBName.Text!="")
            {
                List<Player> filtTemp = new List<Player>();
                foreach(Player p in _filt)
                {
                    if(p.Name.ToLower().Contains(TBName.Text.ToLower()))
                    {
                        filtTemp.Add(p);
                    }
                }
                _filt = filtTemp;
            }

            if(ComBSeason.SelectedItem!="All"&& ComBSeason.SelectedItem != null)
            {
                List<Player> filtTemp = new List<Player>();
                foreach(Player f in _filt)
                {
                    foreach(PlayerInTeam p in dbcl.dbP.PlayerInTeam.Where(x => x.Season.Name == ComBSeason.SelectedItem).ToList())
                    {
                        if(p.PlayerId==f.PlayerId && !filtTemp.Contains(f))
                        {
                            filtTemp.Add(f);
                        }
                    }
                }
                _filt = filtTemp;
            }

            if(ComBTeam.SelectedItem!="All"&&ComBTeam.SelectedItem!=null)
            {
                List<Player> filtTemp = new List<Player>();
                int idTeam = dbcl.dbP.Team.FirstOrDefault(x => x.TeamName == ComBTeam.SelectedItem).TeamId;
                foreach(Player f in _filt)
                {
                    foreach (PlayerInTeam p in dbcl.dbP.PlayerInTeam.Where(x => x.TeamId==idTeam).ToList())
                    {
                        if (p.PlayerId == f.PlayerId && !filtTemp.Contains(f))
                        {
                            filtTemp.Add(f);
                        }
                    }
                }
                _filt = filtTemp;
            }

            if (_filt.Count == 0)
            {
                LVPlayers.ItemsSource = null;
            }
            LVPlayers.Items.Refresh();
            pc.CountItemsInListGetSet = _filt.Count();
            GoToPage(1);
            TBCountPage.Text =""+ pc.CountPagesGetSet;
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
