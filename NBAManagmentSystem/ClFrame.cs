using System.Collections.Generic;
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
        }

        public static void BackPage()
        {
            if(NumThis-1>=0)
            {
                NumThis--;
                Fr.Navigate(History[NumThis]);
            }
        }
    }
}
