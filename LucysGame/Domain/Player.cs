using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    public class Player
    {
        public Dictionary<string, Card> CardDict;
        public string Name { get; internal set; }
        private User User {get; set; } 

        public Player(string _name)
        {
            Name = _name;
            CardDict = new Dictionary<string, Card>();
            CardDict["V1"] = null;
            CardDict["V2"] = null;
            CardDict["H1"] = null;
            CardDict["H2"] = null;

            User = new ComputerUser();
        }

        public List<int> CardValues
        {
            get
            {
                return CardDict.Values.Select(x => x.Number).ToList();
            }
        }

        public int ValV1
        {
            get
            {
                if (CardDict["V1"] == null) return 0;
                return CardDict["V1"].Number;
            }
        }
        public int ValV2
        {
            get
            {
                if (CardDict["V2"] == null) return 0;
                return CardDict["V2"].Number;
            }
        }
        public int ValH1
        {
            get
            {
                if (CardDict["H1"] == null) return 0;
                return CardDict["H1"].Number;
            }
        }
        public int ValH2
        {
            get
            {
                if (CardDict["H2"] == null) return 0;
                return CardDict["H2"].Number;
            }
        }

        public CardChoice PlayerCardChoice(BoardState state)
        {
            return User.ChooseCard(state);
        }

        public CardPlacement PlayerCardPlacement(Card _drawnCard)
        {
            return User.PlaceCard(this.CardValues, _drawnCard.Number);
        }

        internal Card SwapCard(string v, Card newCard)
        {
            Card oldCard = CardDict[v];
            if (v.Contains("V"))
            {
                newCard.Visibility = CardVisibilityState.Public;
            }
            else
            {
                newCard.Visibility = CardVisibilityState.Known;
            }
            CardDict[v] = newCard;
            return oldCard;
        }
    }
}
