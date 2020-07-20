using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUserInterface
{
    public class CardButton : Button
    {
        private char m_Value;
        private int m_IndexOfValue;
        private int m_ButtonIndex;

        public CardButton(int i_Index, char i_Value, int i_ButtonIndex)
        {
            m_IndexOfValue = i_Index;
            m_Value = i_Value;
            m_ButtonIndex = i_ButtonIndex;
        }

        public int ButtonIndex 
        {    
            get 
            { 
                return m_ButtonIndex; 
            }  
        }

        public char Value 
        { 
            get 
            { 
                return m_Value; 
            }
        }

        public int IndexOfValue 
        { 
            get 
            {
                return m_IndexOfValue; 
            } 
        }
        
        public void OpenCard()
        {
            this.Text = m_Value.ToString(); 
        }
    }
}
