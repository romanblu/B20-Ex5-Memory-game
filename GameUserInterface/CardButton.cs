using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUserInterface
{
    class CardButton : Button
    {
        private int m_Rows;
        private int m_Columns;
        private string m_Name;
        private int m_IndexOfValue;

        public CardButton(int i_Index)
        {
            m_IndexOfValue = i_Index;
        }

        public int IndexOfValue { get {return m_IndexOfValue; } }
        public int Columns { get { return m_Columns; } }



    }
}
