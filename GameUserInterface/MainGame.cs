using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameUserInterface
{
    public partial class MainGame : Form
    {
        DialogResult result;//added
        int m_Rows;
        int m_Columns;
        int m_FirstPlayerPairs = 0;
        int m_SecondPlayerPairs = 0;
        CardButton m_FirstCard;
        CardButton m_SecondCard;
        

        Label m_LabelCurrentPlayer = new Label();
        Label m_LabelCurrentPlayerName = new Label();
        Label m_LabelFirstPlayerStats = new Label();
        Label m_LabelSecondPlayerStats = new Label();

        Label m_LabelFirstPlayerName = new Label();
        Label m_LabelSecondPlayerName = new Label();
        
        bool m_ButtonEnable = true;// added
        string m_FirstPlayerName;
        string m_SecondPlayerName;
        string m_CurrentPlayer;
        List<Button> m_Cards = new List<Button>();
        char[] values;
        int[] indexesOfValues;
        GameManager gameManager;
        
        private readonly Color k_FirstPlayerColor = Color.LawnGreen;
        private readonly Color k_SecondPlayerColor = Color.MediumPurple;
        private readonly Color k_ClosedCardColor = Color.DimGray;

        public MainGame(int i_Columns, int i_Rows, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_Rows = i_Rows;
            m_Columns = i_Columns;
            m_FirstPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = i_SecondPlayerName;
            m_CurrentPlayer = i_FirstPlayerName;
            values = new char[i_Rows * i_Columns / 2];
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            m_LabelFirstPlayerName.Text = i_FirstPlayerName +": ";
            m_LabelSecondPlayerName.Text = i_SecondPlayerName +": ";
            m_LabelFirstPlayerStats.Text = "0 Pairs";
            m_LabelSecondPlayerStats.Text = "0 Pairs";
            m_LabelCurrentPlayer.Text = "Current Player:";
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

            m_LabelCurrentPlayer.BackColor = k_FirstPlayerColor;
            m_LabelCurrentPlayerName.BackColor = k_FirstPlayerColor;
            m_LabelFirstPlayerName.BackColor = k_FirstPlayerColor;
            m_LabelFirstPlayerStats.BackColor = k_FirstPlayerColor;
            m_LabelSecondPlayerName.BackColor = k_SecondPlayerColor;
            m_LabelSecondPlayerStats.BackColor = k_SecondPlayerColor;

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
                    int index = m_Columns * i + j;
                    CardButton card = new CardButton(indexesOfValues[index], values[indexesOfValues[index]], index);
                    card.Height = 60;
                    card.Width = 60;
                    card.Location = new Point(10 + (card.Width + 10) * j, 10 + (card.Height + 10) * i);
                    m_Cards.Add(card);
                    card.BackColor = k_ClosedCardColor;
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
            
            if(m_ButtonEnable == true && card.Text == "" && m_CurrentPlayer != "Computer" )//added
            {
                OpenCard(card);
                gameManager.Move(card.ButtonIndex);
                this.Refresh(); 
            }

            if(m_CurrentPlayer == "Computer")
            {
                System.Threading.Thread.Sleep(500);
                OpenCard(card);
                gameManager.Move(card.ButtonIndex);
                this.Refresh();
                System.Threading.Thread.Sleep(500);

            }

            if (gameManager.FirstMove)
            {
                m_FirstCard = card;

                if ( m_CurrentPlayer == "Computer")
                {
                    Console.WriteLine("PC 2nd move");
                    System.Threading.Thread.Sleep(1000);
                    ComputerTurn();
                }
            }
            else 
            {
                
                   m_ButtonEnable = false;
                   m_SecondCard = card; 
                   
                  if (gameManager.WonRound)
                  {
                   // we got a match
                    UpdatePoints(m_CurrentPlayer);

                    if(m_CurrentPlayer == "Computer")
                    {
                        ComputerTurn();
                    }
                  }
                  else
                  {
                    System.Threading.Thread.Sleep(2000);
                    SwitchPlayers();
                    CloseCards();
                    if(m_CurrentPlayer == "Computer")
                    {
                        ComputerTurn();

                    }
                }    
            }

            if (gameManager.GameFinished)
            {
               result = MessageBox.Show(string.Format("{0}\n\n Would you like to play again?", GetResultLine()), "Game Result", MessageBoxButtons.YesNo);
          
               if(result == DialogResult.No)
               {
                   this.Close();
               }
               else if(result == DialogResult.Yes)
               {
                    this.Visible = false;
                    MainGame newGame = new MainGame(m_Columns, m_Rows, m_FirstPlayerName,m_SecondPlayerName);
                    newGame.ShowDialog();
               }
            }
        }
        
         public void ComputerTurn()
         {
            
            this.Refresh();

            int index = gameManager.GetRandomAvaiableIndex();
            (m_Cards.ElementAt(index) as CardButton).PerformClick();

        }

        public string GetResultLine()
         {
            string result;
            if(m_FirstPlayerPairs > m_SecondPlayerPairs)
            {
                result = m_FirstPlayerName + " is the winner!!!!";
            }
            else if((m_SecondPlayerPairs > m_FirstPlayerPairs) && (m_SecondPlayerName != "-computer"))
            {
                result = m_SecondPlayerName + " is the winner!!!!";
            }
            else if(m_SecondPlayerPairs > m_FirstPlayerPairs)
            {
                result = "The computer won";
            }
            else
            {
                result = "It's a draw";
            }

            return result;
        }

        void OpenCard(CardButton i_Card) 
        {
            i_Card.Text = values[i_Card.IndexOfValue].ToString();
            
            if (m_CurrentPlayer == m_FirstPlayerName)
            {
                i_Card.BackColor = k_FirstPlayerColor;
            }

            if (m_CurrentPlayer == m_SecondPlayerName)
            {
                i_Card.BackColor = k_SecondPlayerColor;
            }
        }

        void CloseCards()
        {
            m_FirstCard.Text = "";
            m_SecondCard.Text = "";
            m_FirstCard.BackColor = k_ClosedCardColor;
            m_SecondCard.BackColor = k_ClosedCardColor;
        }

       
        void SwitchPlayers()
        {
            m_ButtonEnable = true;//added
            if (m_CurrentPlayer == m_FirstPlayerName)
            {
                m_CurrentPlayer = m_SecondPlayerName;
                m_LabelCurrentPlayerName.Text = m_CurrentPlayer;
                m_LabelCurrentPlayer.BackColor = k_SecondPlayerColor;
                m_LabelCurrentPlayerName.BackColor = k_SecondPlayerColor;
            }
            else
            {
                m_CurrentPlayer = m_FirstPlayerName;
                m_LabelCurrentPlayerName.Text = m_CurrentPlayer;
                m_LabelCurrentPlayer.BackColor = k_FirstPlayerColor;
                m_LabelCurrentPlayerName.BackColor = k_FirstPlayerColor;
            }
        }

        void UpdatePoints(string i_CurrentPlayer)
        {
            m_ButtonEnable = true;//added
            if(i_CurrentPlayer == m_FirstPlayerName)
            {
                m_FirstPlayerPairs++;
                m_LabelFirstPlayerStats.Text = m_FirstPlayerPairs + " Pairs";
            }
            else
            {
                m_SecondPlayerPairs++;
                m_LabelSecondPlayerStats.Text = m_SecondPlayerPairs + " Pairs";
            }
        }
    }
}
