using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToeApp
{
	public partial class MainPage : ContentPage
	{
        public string Player = "";
        public string Mode = "";

        public MainPage()
        {
            InitializeComponent();
        }


        private void SelectPlayer_Clicked(object sender, EventArgs e)
        {
            Player = ((Button)sender).Text;

            // change colour 
            this.FindByName<Button>("selectPlayerX").BackgroundColor = Color.White;
            this.FindByName<Button>("selectPlayerO").BackgroundColor = Color.White;
            ((Button)sender).BackgroundColor = Color.Teal;
        }


        private void SelectMode_Clicked(object sender, EventArgs e)
        {
            Mode = ((Button)sender).Text;

            // change colour 
            this.FindByName<Button>("modeComputer").BackgroundColor = Color.White;
            this.FindByName<Button>("modeTwoPlayers").BackgroundColor = Color.White;
            ((Button)sender).BackgroundColor = Color.Teal;
        }

        private void StartGame_Clicked(object sender, EventArgs e)
        {
            if (Mode == "" || Player == "")
            {
                DisplayAlert("", "Please select a mode and button", "OK");
            }
            else
            {
                App.Current.MainPage = new PlayerSelection(Mode, Player);
            }
        }
    }
}
