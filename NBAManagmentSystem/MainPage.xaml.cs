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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    
    

    public partial class MainPage : Page
    {
        int _ThisImagesId = 0; //номер первого отображаемого изображения
        int _ImageTaken = 0; //сколько взято
        List<Photos> _photo = new List<Photos>();
        public MainPage()
        {
            InitializeComponent();
            
            foreach(Pictures p in dbcl.dbP.Pictures.ToList())
            {
                _photo.Add(new Photos(p.Id, p.Img));
            }
            LVImage.ItemsSource = _photo.Skip(0).Take(3);
        }

        private void LVImageLoaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.Source = _photo[_ImageTaken + _ThisImagesId].Photo;
            img.Uid = "" + (_ImageTaken+_ThisImagesId);
            _ImageTaken++;
            if(_ImageTaken==3)
            {
                _ImageTaken = 0;
            }
        }

        private void LVImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            Image i = sender as Image;
            ImagePreview p = new ImagePreview(_photo[Convert.ToInt32(i.Uid)].Photo);
            foreach(Window w in Application.Current.Windows)
            {
                if(w.GetType()==typeof(MainWindow))
                {
                    p.Top = w.Top+50; //центрируем открытие изображения по текущему окну
                    p.Left = w.Left;
                    p.ShowDialog();
                    return;
                }
            }
        }

        private void ButtonNextLVImageClick(object sender, RoutedEventArgs e)
        {
            if (2+_ThisImagesId+1 < _photo.Count)
            {
                _ThisImagesId++;
                LVImage.ItemsSource = _photo.Skip(_ThisImagesId).Take(3);
            }

        }

        private void ButtonBackLVImageClick(object sender, RoutedEventArgs e)
        {
            if (_ThisImagesId-1 >0)
            {
                _ThisImagesId--;
                LVImage.ItemsSource = _photo.Skip(_ThisImagesId).Take(3);
            }
        }
    }
}
