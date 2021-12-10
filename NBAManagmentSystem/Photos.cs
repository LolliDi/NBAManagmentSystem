using System.IO;
using System.Windows.Media.Imaging;

namespace NBAManagmentSystem
{
    class Photos
    {
        int Id;
        public BitmapImage Photo = new BitmapImage();

        public byte[] photo
        {
            set 
            {
                using (MemoryStream MS = new MemoryStream(value))
                {
                    Photo.BeginInit();
                    Photo.StreamSource = MS;
                    Photo.CacheOption = BitmapCacheOption.OnLoad;
                    Photo.EndInit();
                }
            }
        }
        public Photos(int id, byte[] b)
        {
            Id = id;
            photo = b;
        }
}
}
