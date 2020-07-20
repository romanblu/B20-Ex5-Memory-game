using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class PlayerStats
    {
        private string m_Name;
        private int m_Points;

        public PlayerStats(string i_Name, int i_Points)
        {
            m_Name = i_Name;
            m_Points = i_Points;
        }

        public static bool ValidateInputName(string i_Name)
        {
            bool valid = true;

            if (i_Name.Length == 0)
            {
                valid = false;
            }
            else 
            {
                for (int i = 0; i < i_Name.Length; i++)
                {
                    if (!(i_Name[i] <= 'z' && i_Name[i] >= 'a') && !(i_Name[i] <= 'Z' && i_Name[i] >= 'A'))
                    {
                        valid = false;
                    }
                }
            }

            return valid;
        }
    }
}
