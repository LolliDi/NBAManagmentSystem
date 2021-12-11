using System;
using System.Linq;

namespace NBAManagmentSystem
{
    public partial class Player
    {


        public int getOld
        {
            get
            {
                DateTime end = DateTime.Today;
                DateTime start = DateOfBirth;
                return end.Year - start.Year - 1 + (((end.Month > start.Month) || ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            }
        }

        public string getTeam
        {
            get
            {
                string[] name = dbcl.dbP.Team.FirstOrDefault(y => y.TeamId == dbcl.dbP.PlayerInTeam.FirstOrDefault(x => x.PlayerId == PlayerId).TeamId).TeamName.Split(' ');
                string retName = "";
                foreach (string s in name)
                {
                    retName += s[0];
                }
                return retName;
            }
        }

        public int getExp
        {
            get
            {
                DateTime end = DateTime.Today;
                DateTime start = JoinYear;
                return end.Year - start.Year - 1 + (((end.Month > start.Month) || ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            }
        }
    }
}
