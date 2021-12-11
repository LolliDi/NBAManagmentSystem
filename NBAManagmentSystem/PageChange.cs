

namespace NBAManagmentSystem
{
    class PageChange //хотел сделать автообновление количества страниц на Visitor menu, но привязка не хотела работать :(
    {
        static int _countItemsOnPage = 4; //количество записей на странице
        int countPages { get; set; } //количество страниц
        int _countItemsInList;//количество записей в листе
        public int thisPageNum; //текущая страница
        public int CountPagesGetSet //присваиваем/получаем количество страниц
        {
            get => countPages;
            set
            {
                countPages = value / _countItemsOnPage + ((value % _countItemsOnPage == 0) ? 0 : 1);

                thisPageNum = 1;
            }
        }

        public int CountItemsInListGetSet
        {
            get => _countItemsInList;
            set
            {
                _countItemsInList = value;
                CountPagesGetSet = value;
            }
        }

    }
}
