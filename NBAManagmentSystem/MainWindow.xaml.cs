using System.Windows;

namespace NBAManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClFrame.Fr = FramePage;
            ClFrame.AddPage(0, new MainPage());

        }

        private void BackPageClick(object sender, RoutedEventArgs e)
        {
            ClFrame.BackPage();
        }
    }
}
