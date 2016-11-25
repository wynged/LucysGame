using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class GameModel
    {
        public Board TheBoard { get; internal set; }
        public List<PlayerModel> Players { get; }

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


            this.AddPlayerModel("Jack");
            this.AddPlayerModel("Jill");
            this.AddPlayerModel("Jane");

            TheBoard.StartGame();
        }

        public void NextTurn()
        {

        }

        public void AddPlayerModel(string _name)
        {
            Players.Add(new PlayerModel(TheBoard.AddPlayer(_name)));
        }
    }
}