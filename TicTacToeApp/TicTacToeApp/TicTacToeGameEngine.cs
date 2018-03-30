using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TicTacToeApp
{
    class TicTacToeGameEngine
    {

        public string aiPlayer = "";
        public string humanPlayer = "";
        public int Choice { get; set; }

        private int[,] Winners = new int[,]
        {
            {0,1,2 },
            {3,4,5 },
            {6,7,8 },
            {0,3,6 },
            {1,4,7 },
            {2,5,8 },
            {0,4,8 },
            {2,4,6 },
        };


        public string SwitchPlayer(string player)
        {
            return player == "X" ? "O" : "X";
        }

        public void SetButton(Button b, string currentPlayer)
        {
            // alternate players
            if (b.Text == "")
            {
                b.Text = currentPlayer;
                b.TextColor = currentPlayer == "X" ? Color.Red : Color.Cyan;
            }
        }

        public bool CheckGameWin(Button[] buttons)
        {
            // if there's a winner 
            for (int i = 0; i < 8; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
                Button b1 = buttons[a], b2 = buttons[b], b3 = buttons[c];

                if (b1.Text == "" || b2.Text == "" || b3.Text == "")
                {
                    continue;
                }

                if (b1.Text == b2.Text && b2.Text == b3.Text)
                {
                    b1.BackgroundColor = b2.BackgroundColor = b3.BackgroundColor = Color.DarkGreen; // matches
                    return true;
                }
            }
            return false;
        }

        private bool CheckGameWin(List<string> board, string player)
        {
            for (int i = 0; i < board.Count - 1; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
                string b1 = board[a], b2 = board[b], b3 = board[c];

                if (b1 == b2 && b2 == b3 && b1 == player) // if there's a winner 
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckGameEnd(Button[] buttons)
        {
            // check if there's a tie 
            foreach (Button b in buttons)
            {
                if (b.Text == "") return false;
            }
            return true;
        }

        private bool CheckGameEnd(List<string> board)
        {
            // check if there's a tie 
            return !board.Where(s => s == "").Select(s => s).Any();
        }

        private int CheckScore(List<string> board, string player, int depth)
        {
            if (CheckGameWin(board, player)) return 10 - depth;
            else if (CheckGameWin(board, SwitchPlayer(player))) return depth - 10;
            else return 0;
        }



        private List<int> GetEmptySpots(List<string> board)
        {
            return board.Select((s, index) => new { s, index }).Where(x => x.s != aiPlayer && x.s != humanPlayer).Select(x => x.index).ToList();
        }

        public int GetBestMove(List<string> board, string player, int depth)
        {
            // minimax algorithm
            List<string> newBoard = board.ToList(); //ButtonsToList(buttons); //copy

            // -- Base case, check terminal state {win, lose, tie}
            var score = CheckScore(newBoard, aiPlayer, depth);
            if (score != 0)
                return score;
            else if (CheckGameEnd(newBoard))
                return 0;

            // -- recursive function 
            List<int> scores = new List<int>();
            List<int> moves = new List<int>();
            depth = depth + 1;

            foreach (int es in GetEmptySpots(newBoard)) // terminate if there's no emptySpots 
            {
                newBoard[es] = player;
                int newScore = GetBestMove(newBoard, SwitchPlayer(player), depth);
                scores.Add(newScore);
                moves.Add(es);
                newBoard[es] = "";

            }

            // -- get best score 
            int bestScore = 0;
            if (player == aiPlayer)
            {
                int MaxScoreIndex = scores.IndexOf(scores.Max());
                Choice = moves[MaxScoreIndex];
                bestScore = scores.Max();
            }
            else
            {
                int MinScoreIndex = scores.IndexOf(scores.Min());
                Choice = moves[MinScoreIndex];
                bestScore = scores.Min();
            }
            return Choice;
        }



        /*
        public Boolean CheckWinner(Button[] buttons, Label playerTurnLabel)
        {
            bool gameOver = false;
            for(int i=0; i< 8; i++) //for each row in Winners || all possible matches 
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
                Button b1 = buttons[a], b2 = buttons[b], b3 = buttons[c];

                if (b1.Text == "" || b2.Text == "" || b3.Text == "")
                {
                    continue;
                }
                    
                if (b1.Text == b2.Text && b2.Text == b3.Text)
                {
                    b1.BackgroundColor = b2.BackgroundColor = b3.BackgroundColor = Color.DarkGreen; // matches
                    gameOver = true;
                    playerTurnLabel.Text = String.Format("Player {0} win!", b1.Text);
                    break; 
                }
            }

            // check if there is a tie  || all buttons are filled 
            bool isTie = true;
            if (!gameOver)
            {
                foreach(Button b in buttons)
                {
                    if (b.Text == "")
                    {
                        isTie = false;
                        break;
                    }
                }

                if (isTie)
                {
                    gameOver = true;
                    playerTurnLabel.Text = String.Format("It's a tie!");
                }
            }
            return gameOver;

        }
        */


        public List<string> ButtonsToList(Button[] buttons)
        {
            List<string> buttonsList = new List<string>(8);
            foreach (Button b in buttons)
            {
                buttonsList.Add(b.Text);
            }
            return buttonsList;
        }


        public void ResetGame(Button[] buttons)
        {
            foreach (Button b in buttons)
            {
                b.Text = "";
                b.BackgroundColor = Color.Black;
            }

        }


    }
}
