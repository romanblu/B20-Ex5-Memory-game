using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameManager
    {
        private int[] m_IndexesOfValues;   // a table that holds the index of a value (letter) for every index
        private bool m_FirstMove = false;
        private Dictionary<int, int> m_AvailableIndexes;
        
        private int m_FirstMoveValue;
        private int m_SecondMoveValue;
        private bool m_WonRound = false;
        private bool m_GameFinished;
        private int m_FirstMoveButtonIndex;
        private int m_SecondMoveButtonIndex;

        public GameManager(int i_Rows, int i_Columns)
        {
            m_IndexesOfValues = new int[i_Rows * i_Columns];
            m_IndexesOfValues = GenerateRandomIndexes(i_Rows, i_Columns);
            
            m_GameFinished = false;
            m_FirstMove = false;
            m_AvailableIndexes = new Dictionary<int, int>(i_Rows * i_Columns);

            for(int i = 0; i < i_Rows * i_Columns; i++)
            {
                m_AvailableIndexes.Add(i, m_IndexesOfValues[i]);
            }
        }

        public bool GameFinished 
        {  
            get 
            { 
                return m_GameFinished; 
            } 
        }
        
        public bool FirstMove 
        { 
            get 
            { 
                return m_FirstMove; 
            } 
        }
        
        public bool WonRound 
        { 
            get 
            { 
                return m_WonRound; 
            } 
        }

        public int[] GenerateRandomIndexes(int i_Rows, int i_Columns)
        {
            int[] indexes = new int[i_Rows * i_Columns];

            for (int i = 0; i < i_Rows * i_Columns / 2; i++)
            {
                indexes[2 * i] = i;
                indexes[(2 * i) + 1] = i;
            }

            Randomize(indexes);
            m_IndexesOfValues = indexes;
            return indexes;
        }

        private static void Randomize(int[] items)
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

        public int GetRandomAvaiableIndex()
        {
            Random random = new Random();
            int a = m_AvailableIndexes.Count;
            int randomIndex = random.Next(0, m_AvailableIndexes.Count);
            return m_AvailableIndexes.ElementAt(randomIndex).Key;
        }
        
        public void Move(int i_ButtonIndex)
        { 
            if (!m_FirstMove)
            {
                m_WonRound = false;
                m_AvailableIndexes.TryGetValue(i_ButtonIndex, out m_FirstMoveValue);
                m_AvailableIndexes.Remove(i_ButtonIndex);
                m_FirstMoveButtonIndex = i_ButtonIndex;
                m_FirstMove = true;
            }
            else
            {
                m_SecondMoveButtonIndex = i_ButtonIndex;
                m_AvailableIndexes.TryGetValue(i_ButtonIndex, out m_SecondMoveValue);
                m_AvailableIndexes.Remove(i_ButtonIndex);
                m_FirstMove = false;
                if(m_FirstMoveValue == m_SecondMoveValue)
                {
                    m_WonRound = true;
                    
                    if (m_AvailableIndexes.Count == 0)
                    {
                        m_GameFinished = true;
                    }
                }
                else
                {
                    m_AvailableIndexes.Add(m_FirstMoveButtonIndex, m_FirstMoveValue);
                    m_AvailableIndexes.Add(m_SecondMoveButtonIndex, m_SecondMoveValue);
                }
            }
        }

        public bool CheckMatch()
        {
            if(m_FirstMoveValue == m_SecondMoveValue)
            {
                if(m_AvailableIndexes.Count == 0)
                {
                    m_GameFinished = true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
