using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUserInterface
{
    public partial class MainGame : Form
    {
        int m_Rows;
        int m_Columns;

        List<Button> m_Cards = new List<Button>();

        public MainGame(int i_Columns, int i_Rows)
        {
            m_Rows = i_Rows;
            m_Columns = i_Columns;

            this.Size = new Size(400, 300);
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitializeCards();

        }

        void InitializeCards()
        {
            for(int i=0; i < m_Columns; i++)
            {
                for(int j=0; j < m_Rows; i++)
                {
                    Button card = new Button();
                    card.Text = i + j .ToString();
                    card.Height = 30;
                    card.Width = 30;
                    card.Location = new Point(10 + (card.Width + 10) * i, 10 + (card.Height + 10) * j);
                    this.Controls.Add(card);
                }
            }
        }
    }
}
