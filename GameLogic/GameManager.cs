
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameManager
    {

        private int[] m_IndexesOfValues;   // a table that holds the idex of a value (letter) for every index
        private bool m_FirstMove = false;
        //List<int> m_AvailableCards;
        private int m_FirstMoveValue;
        private int m_SecondMoveValue;
        private int m_AvailableCards;
        private bool m_WonRound = false;

        public GameManager(int i_Rows, int i_Columns)
        {
            m_IndexesOfValues = new int[i_Rows * i_Columns];
            m_AvailableCards = i_Rows * i_Columns;
            /*
            for(int i=0;i< i_Rows * i_Columns; i++)
            {
                m_AvailableCards.Add(i);
            }*/
        }

        public bool FirstMove { get { return m_FirstMove; } }
        public bool WonRound { get { return m_WonRound; } }
        
        public int[] GenerateRandomIndexes(int i_Rows, int i_Columns)
        {
            int[] indexes = new int[i_Rows * i_Columns];
            
            
            for(int i=0; i< i_Rows * i_Columns / 2; i++)
            {
                indexes[2 * i] = i;
                indexes[(2 * i) + 1] = i+1;
                
            }

           // Randomize(indexes);
            
            m_IndexesOfValues = indexes;

            return indexes;
        }

        static void Randomize(int[] items)
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

        
        public void Move(int i_IndexOfValue)
        {
            if (!m_FirstMove)
            {
                m_WonRound = false;
                m_AvailableCards--;
                m_FirstMoveValue = i_IndexOfValue;
                m_FirstMove = true;
            }
            else
            {
                m_AvailableCards--;
                m_SecondMoveValue = i_IndexOfValue;
                m_FirstMove = false;
                if(m_FirstMoveValue == m_SecondMoveValue)
                {
                    m_WonRound = true;
                }
            }

        }

        public bool CheckMatch()
        {
            if(m_FirstMoveValue == m_SecondMoveValue)
            {
                return true;
            }
            else
            {
                m_AvailableCards += 2;
                return false;
            }

        }


    }
}
