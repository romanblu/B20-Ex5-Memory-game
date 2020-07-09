
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameManager
    {

        int[] m_IndexesOfValues;   // a table that holds the idex of a value (letter) for every index
        bool m_FirstMove = false;
        //List<int> m_AvailableCards;
        int m_FirstMoveValue;
        int m_SecondMoveValue;
        int m_AvailableCards;

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
        public void OpenCard(int i_Index)
        {
            //m_AvailableCards.RemoveAt(m_AvailableCards.ElementAt(i_Index)); // getting the index of the element we want to remove and remove it
        }
        // close card
        public void CloseCard(int i_Index)
        {
           // m_AvailableCards.Add(i_Index);
        }
        // if match
        public void Move(int i_IndexOfValue)
        {
            if (!m_FirstMove)
            {
                //OpenCard(i_IndexOfValue);
                m_AvailableCards--;
                m_FirstMoveValue = i_IndexOfValue;
                m_FirstMove = true;
            }
            else
            {
                //OpenCard(i_Index);
                m_AvailableCards--;
                //m_SecondMoveValue = m_IndexesOfValues[i_IndexOfValue];
                m_SecondMoveValue = i_IndexOfValue;
                m_FirstMove = false;
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

        // against PC 

    }
}
