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
using System.Windows.Shapes;

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
