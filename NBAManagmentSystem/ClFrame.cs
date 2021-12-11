using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NBAManagmentSystem
{
    class ClFrame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static Frame Fr;
        public static List<object> History = new List<object>();
        public static int NumThis = 0;
        public static int CountPage = 0;
        public string thisPage { get; set; }

        public string ThisPage
        {
            get => thisPage;
            set
            {
                thisPage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(thisPage));
            }
        }

        public static void AddPage(int ind, object page) //добавление страницы в историю
        {
            if (ind < CountPage) //удаляем страницы, если они идут после индекса вставки
            {
                for (int i = ind; i < CountPage; CountPage--)
                {
                    History.RemoveAt(i);
                }
            }
            History.Add(page);
            NumThis = ind;
            CountPage = History.Count;
            Fr.Navigate(History[NumThis]);
            prover(History[NumThis]);
        }

        public static void BackPage()
        {
            if (NumThis - 1 >= 0)
            {
                NumThis--;
                Fr.Navigate(History[NumThis]);
                prover(History[NumThis]);
            }
        }

        static void prover(object o) //Проверяем какое это окно, если меню, то скрываем шапку, иначе показываем её
        {
            MainWindow m = null;
            foreach (Window w in Application.Current.Windows) //ищем текущее окно
            {
                if (w.GetType() == typeof(MainWindow))
                {
                    m = w as MainWindow;
                    break;
                }
            }
            if (o.GetType() == typeof(MainPage))
            {
                m.GridCap.Height = new GridLength(0);
                m.StackCap.Visibility = Visibility.Collapsed;
            }
            else
            {
                m.GridCap.Height = new GridLength(69, GridUnitType.Star);
                m.StackCap.Visibility = Visibility.Visible;
            }

            switch (o.GetType().Name)
            {
                case nameof(VisitorMenu):
                    m.TBNamePage.Text = "Visitor Menu";
                    break;
                case nameof(PlayersPage):
                    m.TBNamePage.Text = "Players Page";
                    break;
                case nameof(PhotoPage):
                    m.TBNamePage.Text = "Photos";
                    break;
                case nameof(MatchupPage):
                    m.TBNamePage.Text = "Matchup List";
                    break;
                case nameof(TeamsPage):
                    m.TBNamePage.Text = "Teams Main";
                    break;
                default:
                    m.TBNamePage.Text = o.GetType().Name;
                    break;
            }
        }
    }
}
