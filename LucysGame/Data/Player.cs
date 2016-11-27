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
        private User User {get; set; } 

        public Player(string _name)
        {
            Name = _name;
            Cards = new Dictionary<string, Card>();
            Cards["V1"] = null;
            Cards["V2"] = null;
            Cards["H1"] = null;
            Cards["H2"] = null;

            User = new ComputerUser();
        }

        public List<int> CardValues
        {
            get
            {
                return Cards.Values.Select(x => x.Number).ToList();
            }
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

        public CardChoice PlayerCardChoice(Card _discardCard)
        {
            return User.ChooseCard(this.CardValues, _discardCard.Number);
        }

        public CardPlacement PlayerCardPlacement(Card _drawnCard)
        {
            return User.PlaceCard(this.CardValues, _drawnCard.Number);
        }

        internal Card SwapCard(string v, Card newCard)
        {
            Card oldCard = Cards[v];
            Cards[v] = newCard;
            return oldCard;
        }
    }
}
