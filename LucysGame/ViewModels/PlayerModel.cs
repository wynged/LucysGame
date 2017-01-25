using System;
using System.Linq;
using LucysGame.Domain;
using LucysGame.User;

namespace LucysGame.ViewModels
{
    internal class PlayerModel : ViewModelBase
    {
        public Player Player { get; internal set; }
        
        public string Name
        {
            get
            {
                return Player.Name;
            }
        }

        public CardChoice ComputerChooseCard(BoardState state)
        {
            return RandomUser.ChooseCard(state);
        }

        public CardPlacement ComputerCardPlacement(Card _drawnCard)
        {
            return RandomUser.PlaceCard(Player.CardValues, _drawnCard.Number);
        }

        CardModel[] Cards;

        public PlayerModel(Player p)
        {
            Player = p;
            Cards = p.CardDict.Values.Select(c => new CardModel(c)).ToArray();
        }

        public string CardValues
        {
            get
            {
                return String.Join("-", Cards.Select(cm => cm.CardVal));
            }
        }

        public int TotalScore { get
            {
                int total = Cards.Select(cm => cm.TrueVal).Sum();
                return total;
            } }
    }
}