using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NBAManagmentSystem
{
    public partial class Matchup
    {
        public SolidColorBrush OverOrNoColor
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    case 1:
                        return new SolidColorBrush(Color.FromArgb(255, 144, 144, 144));
                    default:
                        return new SolidColorBrush(Color.FromArgb(255, 30, 144, 255));
                }
            }
        }

        public string OverOrNoText
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "Running";
                    case 1:
                        return "Finished";
                    default:
                        return "Not Start";
                }
            }
        }

        public BitmapImage GetLogoAway
        {
            get
            {
                BitmapImage Photo = new BitmapImage();
                byte[] buff = dbcl.dbP.Team.First(x => x.TeamId == Team_Away).Logo;
                using (MemoryStream MS = new MemoryStream(buff))
                {
                    Photo.BeginInit();
                    Photo.StreamSource = MS;
                    Photo.CacheOption = BitmapCacheOption.OnLoad;
                    Photo.EndInit();
                }
                return Photo;
            }
        }

        public BitmapImage GetLogoHome
        {
            get
            {
                BitmapImage Photo = new BitmapImage();
                byte[] buff = dbcl.dbP.Team.First(x => x.TeamId == Team_Home).Logo;
                using (MemoryStream MS = new MemoryStream(buff))
                {
                    Photo.BeginInit();
                    Photo.StreamSource = MS;
                    Photo.CacheOption = BitmapCacheOption.OnLoad;
                    Photo.EndInit();
                }
                return Photo;
            }
        }

        public string GetNameAway
        {
            get
            {
                return dbcl.dbP.Team.First(x => x.TeamId == Team_Away).TeamName;
            }
        }
        public string GetNameHome
        {
            get
            {
                return dbcl.dbP.Team.First(x => x.TeamId == Team_Home).TeamName;
            }
        }

        public string GetScore
        {
            get
            {
                switch (Status)
                {
                    case -1:
                        return "-";
                    default:
                        return "" + Team_Away_Score + "-" + Team_Home_Score;
                }

            }
        }

    }
}
