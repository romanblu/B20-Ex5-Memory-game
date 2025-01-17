﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameUserInterface
{
    public partial class GameSettings : Form
    {
        private const int k_MinRowLength = 4;
        private const int k_MaxRowLength = 6;
        private const int k_MinColumnLength = 4;
        private const int k_MaxColumnLength = 6;

        TextBox m_TextboxFirstPlayer = new TextBox();
        TextBox m_TextboxSecondPlayer = new TextBox();

        Label m_LabelFirstPlayer = new Label();
        Label m_LabelSecondPlayer = new Label();
        Label m_LabelBoardSize = new Label();

        Button m_ButtonPlayAgainstFriend = new Button();
        Button m_ButtonBoardSize = new Button();
        Button m_ButtonStart = new Button();

        private bool m_AgainstFriend = false;

        public GameSettings()
        {
            this.Size = new Size(400, 300);
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

            this.Controls.AddRange(new Control[] 
            { 
                m_LabelFirstPlayer, m_LabelSecondPlayer,
                m_LabelBoardSize, m_TextboxFirstPlayer, m_TextboxSecondPlayer, m_ButtonPlayAgainstFriend, m_ButtonBoardSize, m_ButtonStart 
            });

            m_ButtonPlayAgainstFriend.Click += m_ButtonPlayAgainstFriend_Click;
            m_ButtonBoardSize.Click += m_ButtonBoardSize_Click;
            m_ButtonStart.Click += m_ButtonStart_Click;
        }

        private void m_ButtonPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            if (m_AgainstFriend)
            {
                m_AgainstFriend = false;
                m_TextboxSecondPlayer.Enabled = false;
                m_TextboxSecondPlayer.Text = "-computer-";
                m_ButtonPlayAgainstFriend.Text = "Against a Friend";
            }
            else
            {
                m_AgainstFriend = true;
                m_TextboxSecondPlayer.Enabled = true;
                m_TextboxSecondPlayer.Text = string.Empty;
                m_ButtonPlayAgainstFriend.Text = "Against Computer";
            }
        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            if (!PlayerStats.ValidateInputName(m_TextboxFirstPlayer.Text))
            {
                MessageBox.Show("Player name not valid, use only characters");
            }
            else if (m_AgainstFriend && !PlayerStats.ValidateInputName(m_TextboxSecondPlayer.Text))
            {
                MessageBox.Show("Player name not valid, use only characters");
            }
            else
            {
                this.Close();   
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (!m_AgainstFriend)
            {
                m_TextboxSecondPlayer.Text = "Computer";
            }

            this.Visible = false;
            int rows = m_ButtonBoardSize.Text[0] - '0';
            int columns = m_ButtonBoardSize.Text[2] - '0';
            MainGame mainGame = new MainGame(columns, rows, m_TextboxFirstPlayer.Text, m_TextboxSecondPlayer.Text);
            mainGame.ShowDialog();
        }

        private void m_ButtonBoardSize_Click(object sender, EventArgs e)
        {
            string targetSize = (sender as Button).Text;
            int rows = targetSize[2] - '0';
            int columns = targetSize[0] - '0';
            rows++;
            if(rows > k_MaxRowLength)
            {
                rows = k_MaxRowLength;
                columns++;
            }

            if(columns > k_MaxColumnLength)
            {
                columns = k_MinColumnLength;
                rows = k_MinRowLength;
            }

            m_ButtonBoardSize.Text = columns + "x" + rows;
        }   
    }
}
