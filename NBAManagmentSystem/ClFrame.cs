using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NBAManagmentSystem
{
    class ClFrame
    {
        public static Frame Fr;
        public static List<object> History=new List<object>();
        public static int NumThis=0;
        public static int CountPage=0;

        public static void AddPage(int ind, object page) //добавление страницы в историю
        {
            if(ind<CountPage) //удаляем страницы, если они идут после индекса вставки
            {
                for(int i=ind;i<CountPage;CountPage--)
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
            if(NumThis-1>=0)
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
        }
    }
}
