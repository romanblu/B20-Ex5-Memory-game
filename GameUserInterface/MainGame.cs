using GameLogic;
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
        
        Label m_LabelCurrentPlayer = new Label();
        Label m_LabelCurrentPlayerName = new Label();
        Label m_LabelFirstPlayerStats = new Label();
        Label m_LabelSecondPlayerStats = new Label();

        Label m_LabelFirstPlayerName = new Label();
        Label m_LabelSecondPlayerName = new Label();

        
        List<Button> m_Cards = new List<Button>();
        char[] values;
        int[] indexesOfValues;
        GameManager gameManager;

        public MainGame(int i_Columns, int i_Rows, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_Rows = i_Rows;
            m_Columns = i_Columns;
            values = new char[i_Rows * i_Columns / 2];
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            m_LabelFirstPlayerName.Text = i_FirstPlayerName +": ";
            m_LabelSecondPlayerName.Text = i_SecondPlayerName +": ";
            m_LabelFirstPlayerStats.Text = "0 Pairs";
            m_LabelSecondPlayerStats.Text = "0 Pairs";
            m_LabelCurrentPlayer.Text = "Current Player: ";
            m_LabelCurrentPlayerName.Text = i_FirstPlayerName;
            indexesOfValues = new int[i_Columns * i_Rows];
            gameManager = new GameManager(i_Rows, i_Columns);
            GenerateValues();
            indexesOfValues = gameManager.GenerateRandomIndexes(i_Rows, i_Columns);
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitializeComponents();
        }

        void InitializeComponents()
        {

            InitializeCards();

            m_LabelCurrentPlayer.AutoSize = true;
            m_LabelCurrentPlayerName.AutoSize = true;
            m_LabelFirstPlayerName.AutoSize = true;
            m_LabelSecondPlayerName.AutoSize = true;
            m_LabelFirstPlayerStats.AutoSize = true;
            m_LabelSecondPlayerStats.AutoSize = true;

            m_LabelCurrentPlayer.BackColor = Color.LimeGreen;
            m_LabelCurrentPlayerName.BackColor = Color.LimeGreen;
            m_LabelFirstPlayerName.BackColor = Color.LimeGreen;
            m_LabelFirstPlayerStats.BackColor = Color.LimeGreen;
            m_LabelSecondPlayerName.BackColor = Color.BlueViolet;
            m_LabelSecondPlayerStats.BackColor = Color.BlueViolet;

            m_LabelCurrentPlayer.Location = new Point(10, m_Cards.ElementAt(m_Cards.Count()-1).Top + m_Cards.ElementAt(m_Cards.Count() - 1).Height + 15);
            m_LabelCurrentPlayerName.Location = new Point(m_LabelCurrentPlayer.Left + m_LabelCurrentPlayer.PreferredWidth, m_LabelCurrentPlayer.Top);
            
            m_LabelFirstPlayerName.Location = new Point(m_LabelCurrentPlayer.Left, m_LabelCurrentPlayer.Bottom);
            m_LabelFirstPlayerStats.Location = new Point(m_LabelFirstPlayerName.Left + m_LabelFirstPlayerName.PreferredWidth, m_LabelFirstPlayerName.Top);

            m_LabelSecondPlayerName.Location = new Point(m_LabelCurrentPlayer.Left, m_LabelFirstPlayerName.Bottom);
            m_LabelSecondPlayerStats.Location = new Point(m_LabelSecondPlayerName.Left + m_LabelSecondPlayerName.PreferredWidth, m_LabelSecondPlayerName.Top);

            this.Controls.AddRange(new Control[] { m_LabelSecondPlayerStats, m_LabelSecondPlayerName, m_LabelCurrentPlayer, m_LabelCurrentPlayerName, m_LabelFirstPlayerName, m_LabelFirstPlayerStats });

            this.Size = new Size( m_Columns * ( 60 + 10) + 20 , m_LabelSecondPlayerStats.Bottom + 50);
        
        
        }

        private void InitializeCards()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Columns; j++)
                {

                    CardButton card = new CardButton(indexesOfValues[m_Rows * i + j]);
                    card.Text = indexesOfValues[m_Rows * i + j].ToString();
                    card.Height = 60;
                    card.Width = 60;
                    card.Location = new Point(10 + (card.Width + 10) * j, 10 + (card.Height + 10) * i);
                    m_Cards.Add(card);
                    card.Click += m_ButtonCard_Click;
                    this.Controls.Add(card);

                }
            }
        }

        private void GenerateValues()
        {
            for(int i = 0; i < m_Rows * m_Columns / 2; i++)
            {
                values[i] = (char)('A' + i);
            }
        }

        void m_ButtonCard_Click(object sender, EventArgs e)
        {
            CardButton card = sender as CardButton;
            
            gameManager.OpenCard(card.IndexOfValue);

            // if first move
            // open card in the logic , update ui

            // if second move , dont match
            // open card in logic and if doesnt match close the cards in the logic and ui 

            // if second move and match 
            // remove the cards from the available list in logic and ui dont close them , leave the same player for the 2nd move and add a point 

            // Block unwanted inputs 


            
           // (sender as CardButton).Text = values[(sender as CardButton).IndexOfValue].ToString();
           /*
            if(m_LabelCurrentPlayerName.Text == m_LabelFirstPlayerName.Text)
            {
                (sender as CardButton).BackColor = Color.LawnGreen;
            }
            else if(m_LabelCurrentPlayerName.Text == m_LabelSecondPlayerName.Text)
            {
                (sender as CardButton).BackColor = Color.MediumPurple;
            }
           */
        }

    }
}
