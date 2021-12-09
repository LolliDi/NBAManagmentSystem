using System.IO;
using System.Windows.Media.Imaging;

namespace NBAManagmentSystem
{
    class Photos
    {
        int Id;
        public BitmapImage Photo = new BitmapImage();
        public Photos(int id, byte[] b)
        {
            Id = id;
            using(MemoryStream MS = new MemoryStream(b))
            {
                Photo.BeginInit();
                Photo.StreamSource = MS;
                Photo.CacheOption = BitmapCacheOption.OnLoad;
                Photo.EndInit();
            }
        }
}
}
