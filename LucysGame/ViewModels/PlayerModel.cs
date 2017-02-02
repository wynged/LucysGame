using System;
using System.Linq;
using LucysGame.Domain;
using LucysGame.Users;

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
            switch (this.Player.Type)
            {
                case (PlayerType.Random):
                    return RandomUser.ChooseCard(state);
                case (PlayerType.Deep):
                    return DeepNNUser.ChooseCard(state);
                default:
                    return CardChoice.MainDeck;
            }
        }

        public CardPlacement ComputerCardPlacement(Card _drawnCard)
        {
            switch(this.Player.Type)
            {

                case (PlayerType.Random):
                    return RandomUser.PlaceCard(Player.CardValues, _drawnCard.Number);
                case (PlayerType.Deep):
                    return DeepNNUser.PlaceCard(Player.CardValues, _drawnCard.Number);
                default:
                    return CardPlacement.Discard;

            }
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