using System.Windows;
using System.Windows.Media.Imaging;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для ImagePreview.xaml
    /// </summary>
    public partial class ImagePreview : Window
    {
        public ImagePreview()
        {
            InitializeComponent();
        }
        public ImagePreview(BitmapImage im)
        {
            InitializeComponent();
            ImagePrev.Source = im;
        }
    }
}
