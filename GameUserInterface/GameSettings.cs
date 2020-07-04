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
    public partial class GameSettings : Form
    {

        TextBox m_TextboxFirstPlayer = new TextBox();
        TextBox m_TextboxSecondPlayer = new TextBox();

        Label m_LabelFirstPlayer = new Label();
        Label m_LabelSecondPlayer = new Label();
        Label m_LabelBoardSize = new Label();

        Button m_ButtonPlayAgainstFriend = new Button();
        Button m_ButtonBoardSize = new Button();
        Button m_ButtonStart = new Button();

        bool m_AgainstFriend = false;

        public GameSettings()
        {
            this.Size = new Size(400, 300);
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";

        }

        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();

        }

        private void InitControls()
        {
            m_LabelFirstPlayer.Text = "First Player Name:";
            m_LabelFirstPlayer.Location = new Point(10, 20);

            m_LabelSecondPlayer.Text = "Second Player Name:";
            m_LabelSecondPlayer.Location = new Point(10, 50);
            m_LabelSecondPlayer.Width = 120;

            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(10, m_LabelSecondPlayer.Top + m_LabelSecondPlayer.Height + 10);

            m_TextboxFirstPlayer.Location = new Point(m_LabelFirstPlayer.Right + 40, m_LabelFirstPlayer.Top);
            m_TextboxSecondPlayer.Location = new Point(m_LabelFirstPlayer.Right + 40, m_LabelSecondPlayer.Top);
            m_TextboxSecondPlayer.Enabled = false;
            m_TextboxSecondPlayer.Text = "-computer-";


            m_ButtonPlayAgainstFriend.Text = "Against a Friend";
            m_ButtonPlayAgainstFriend.Location = new Point(m_TextboxSecondPlayer.Right + 10, m_TextboxSecondPlayer.Top);
            m_ButtonPlayAgainstFriend.Width = 120;
            m_ButtonBoardSize.Location = new Point(10, m_LabelBoardSize.Top + 30);
            m_ButtonBoardSize.Text = "4x4";
            m_ButtonBoardSize.Width = 100;
            m_ButtonBoardSize.Height = 75;
            m_ButtonBoardSize.BackColor = Color.MediumPurple;

            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Location = new Point(m_ButtonPlayAgainstFriend.Right - m_ButtonStart.Width, m_ButtonBoardSize.Top + m_ButtonBoardSize.Height - m_ButtonStart.Height);
            m_ButtonStart.BackColor = Color.LightGreen;

            this.Controls.AddRange(new Control[] { m_LabelFirstPlayer, m_LabelSecondPlayer,
                m_LabelBoardSize, m_TextboxFirstPlayer, m_TextboxSecondPlayer, m_ButtonPlayAgainstFriend, m_ButtonBoardSize, m_ButtonStart });

            m_ButtonPlayAgainstFriend.Click += m_ButtonPlayAgainstFriend_Click;
            m_ButtonBoardSize.Click += m_ButtonBoardSize_Click;

        }

        void m_ButtonPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            if (m_AgainstFriend)
            {
                m_AgainstFriend = false;
                m_TextboxSecondPlayer.Enabled = false;
                m_TextboxSecondPlayer.Text = "-computer-";
            }
            else
            {
                m_AgainstFriend = true;
                m_TextboxSecondPlayer.Enabled = true;
                m_TextboxSecondPlayer.Text = "";
            }
        }

        void m_ButtonBoardSize_Click(object sender, EventArgs e)
        {
            string targetSize = (sender as Button).Text;
            int rows = targetSize[2] - '0';
            int columns = targetSize[0] - '0';
            rows++;
            if(rows > 6)
            {
                rows = 4;
                columns++;
            }
            if( columns > 6)
            {
                columns = 4;
                rows = 4;
            }
            m_ButtonBoardSize.Text = columns + "x" + rows;
        }

    }
}
