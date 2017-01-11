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
            return ComputerUser.ChooseCard(state);
        }

        public CardPlacement ComputerCardPlacement(Card _drawnCard)
        {
            return ComputerUser.PlaceCard(Player.CardValues, _drawnCard.Number);
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
                int total = Cards.Select(cm => int.Parse(cm.CardVal)).Sum();
                return total;
            } }
    }
}