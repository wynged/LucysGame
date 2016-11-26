using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    public class Player
    {
        public Dictionary<string, Card> Cards;
        public string Name { get; internal set; }

        public Player(string _name)
        {
            Name = _name;
            Cards = new Dictionary<string, Card>();
            Cards["V1"] = null;
            Cards["V2"] = null;
            Cards["H1"] = null;
            Cards["H2"] = null;
        }

        public int ValV1
        {
            get
            {
                if (Cards["V1"] == null) return 0;
                return Cards["V1"].Number;
            }
        }
        public int ValV2
        {
            get
            {
                if (Cards["V2"] == null) return 0;
                return Cards["V2"].Number;
            }
        }
        public int ValH1
        {
            get
            {
                if (Cards["H1"] == null) return 0;
                return Cards["H1"].Number;
            }
        }
        public int ValH2
        {
            get
            {
                if (Cards["H2"] == null) return 0;
                return Cards["H2"].Number;
            }
        }

        internal void TakeActions(Board board)
        {
            //choose where to draw from

            //choose where to place card
        }
    }
}
