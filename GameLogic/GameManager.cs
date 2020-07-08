
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameManager
    {

        int[] m_IndexesOfValues;

        public GameManager(int i_Rows, int i_Columns)
        {
            m_IndexesOfValues = new int[i_Rows * i_Columns];
        }

        public int[] GenerateRandomIndexes(int i_Rows, int i_Columns)
        {
            int[] indexes = new int[i_Rows * i_Columns];
            
            for(int i=0;i< i_Rows * i_Columns / 2; i++)
            {
                indexes[2 * i] = i;
                indexes[2 * i + 1] = i;
            }

            Randomize(indexes);
            
            m_IndexesOfValues = indexes;

            return indexes;
        }

        public static void Randomize(int[] items)
        {
            Random rand = new Random();

            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                int temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }

        // make a list of available cards

        // open card

        // close card

        // if match
        
        // against PC 

    }
}
