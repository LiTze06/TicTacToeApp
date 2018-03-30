using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToeApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerSelection : ContentPage
	{
        private Button[] buttons = new Button[9];
        private TicTacToeGameEngine game = new TicTacToeGameEngine();

        public string Mode = "";
        public string Player = "";
        public bool GameEnd = false;

        public PlayerSelection ()
		{
			InitializeComponent ();
		}


        public PlayerSelection(string mode, string player)
        {
            InitializeComponent();

            Mode = mode;
            Player = player;

            if (Mode == "Computer")
            {
                game.aiPlayer = game.SwitchPlayer(Player);
                game.humanPlayer = player;

                player1Label.Text = String.Format("You are: {0}", player);
                player2Label.Text = String.Format("Computer is: {0}", game.aiPlayer);
            }
            else
            {
                player1Label.Text = String.Format("Player 1: {0}", player);
                player2Label.Text = String.Format("Player2: {0}", game.SwitchPlayer(Player));
            }

            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;

            playerTurnLabel.Text = String.Format("{0} Turn", Player);
        }




        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!GameEnd)
            {

                // -- To prevent button from clicking more than one
                if (((Button)sender).Text != "")
                {
                    return;
                }

                // --
                game.SetButton((Button)sender, Player);

                if (Mode == "Computer")
                {
                    var board = game.ButtonsToList(buttons);
                    int bestScore = game.GetBestMove(board, game.humanPlayer, 0);
                    var bestMoveButton = this.FindByName<Button>(String.Format("button{0}", game.Choice + 1));
                    game.SetButton((Button)bestMoveButton, game.aiPlayer);
                }
                else
                {
                    Player = game.SwitchPlayer(Player);
                    playerTurnLabel.Text = Player == "X" ? "X turn" : "O turn";
                }

                // -- Check terminate state 
                if (game.CheckGameWin(buttons) == true)
                {
                    playerTurnLabel.Text = String.Format("Player {0} win!", Player);
                    GameEnd = true;
                }
                else if (game.CheckGameWin(buttons) == false)
                {
                    if (game.CheckGameEnd(buttons))
                    {
                        playerTurnLabel.Text = String.Format("It's a tie", Player);
                        GameEnd = true;
                    }
                }


            }

        }


        private void BackButton_Clicked(object sender, EventArgs e)
        {
            game.ResetGame(buttons);
            GameEnd = false;
            App.Current.MainPage = new MainPage();
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            game.ResetGame(buttons);
            GameEnd = false;
            playerTurnLabel.Text = String.Format("{0} Turn", Player);
        }

    }
}