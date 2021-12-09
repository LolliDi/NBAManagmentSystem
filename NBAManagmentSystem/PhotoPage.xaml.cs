using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
//using WinForms = System.Windows.Forms;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для PhotoPage.xaml
    /// </summary>
    public partial class PhotoPage : Page
    {
        int _ThisImagesId = 0;
        List<Photos> _photo = new List<Photos>();
        public PhotoPage()
        {
            InitializeComponent();
            foreach (Pictures p in dbcl.dbP.Pictures.ToList())
            {
                _photo.Add(new Photos(p.Id, p.Img));
            }
            LVImage.ItemsSource = _photo;
        }

        private void LVImageLoaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.Source = _photo[_ThisImagesId].Photo;
            img.Uid = "" + _ThisImagesId;
            _ThisImagesId++;
        }

        private void LVImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            Image i = sender as Image;
            ImagePreview p = new ImagePreview(_photo[Convert.ToInt32(i.Uid)].Photo);
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(MainWindow))
                {
                    p.Top = w.Top + 50; //центрируем открытие изображения по текущему окну
                    p.Left = w.Left;
                    p.ShowDialog();
                    return;
                }
            }
        }
        /*FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                Save(_photo[Convert.ToInt32(i.Uid)].Photo, FBD.SelectedPath);
    }*/
    private void LVImageDownloadClick(object sender, MouseButtonEventArgs e)
        {

        }

        public void Save(BitmapImage image, string filePath, string uid)
        {
            try
            {

                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                filePath += "\\" + uid + ".jpg";
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
                MessageBox.Show("Файл сохранен!\nПуть к файлу: " + filePath, "Успех!");            
            }
            catch (Exception ee)
            {
                MessageBox.Show(""+ee, "Ошибка");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDownloadClick(object sender, RoutedEventArgs e)
        {
            MenuItem i = sender as MenuItem;
            ContextMenu i1 = i.Parent as ContextMenu;
            Image i2 = i1.PlacementTarget as Image;
            /*OpenFileDialog dial = new OpenFileDialog();
            dial.ShowDialog();
            string path = dial.InitialDirectory;
            Save(_photo[Convert.ToInt32(i.Uid)].Photo,path);*/

            System.Windows.Forms.FolderBrowserDialog FBD = new System.Windows.Forms.FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Save(_photo[Convert.ToInt32(i2.Uid)].Photo, FBD.SelectedPath, i2.Uid);
            }

        }
    }
}
