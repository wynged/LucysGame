using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class GameModel : ViewModelBase
    {
        public Board TheBoard { get; internal set; }
        public List<PlayerModel> Players { get; }
        public ButtonCommand NextTurnCommand { get; }

        public string CurrentPlayerModel
        {
            get
            {
                return Players.Where(x => x.Player == TheBoard.CurrentPlayer).First().Name;
            }
        }

        public List<Card> MainDeckCards
        {
            get
            {
                return TheBoard.MainDeck;
            }
        }

        public GameModel()
        {
            TheBoard = new Board();

            Players = new List<PlayerModel>();

            NextTurnCommand = new ButtonCommand(NextTurn, ReadyForNextTurn);

            this.AddPlayerModel("Jack");
            this.AddPlayerModel("Jill");
            this.AddPlayerModel("Jane");

            TheBoard.StartGame();
        }

        public void NextTurn()
        {
            TheBoard.NextPlayerAction();
            this.SetPropertyChanged("CurrentPlayerModel");
        }

        public bool ReadyForNextTurn()
        {
            return true;
        }

        public void AddPlayerModel(string _name)
        {
            Players.Add(new PlayerModel(TheBoard.AddPlayer(_name)));
        }
    }
}