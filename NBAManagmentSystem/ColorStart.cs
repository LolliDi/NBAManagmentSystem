using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NBAManagmentSystem
{
    public partial class Matchup
    {

        public SolidColorBrush OverOrNoColor
        {
            get
            {
                switch(Status)
                {                  
                    case 0:
                        return new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
                    case 1:
                        return new SolidColorBrush(Color.FromArgb(100, 144, 144, 144));
                    default:
                        return new SolidColorBrush(Color.FromArgb(100, 30, 144, 255));
                }
            }
        }

        public string OverOrNoText
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "Running";
                    case 1:
                        return "Finished";
                    default:
                        return "Not Start";
                }
            }
        }

    }
}
