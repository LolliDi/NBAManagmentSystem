using System.IO;
using System.Windows.Media.Imaging;

namespace NBAManagmentSystem
{
    public partial class Team
    {

        public BitmapImage GetLogo
        {
            get
            {
                BitmapImage Photo = new BitmapImage();
                byte[] buff = Logo;
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
    }
}
