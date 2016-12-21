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

        public CardChoice PlayerCardChoice(BoardState state)
        {
            return ComputerUser.ChooseCard(state);
        }

        public CardPlacement PlayerCardPlacement(Card _drawnCard)
        {
            return ComputerUser.PlaceCard(Player.CardValues, _drawnCard.Number);
        }


        CardModel[] Cards;

        public PlayerModel(Player p)
        {
            Player = p;
            Cards = p.CardDict.Values.Select(c => new CardModel(c)).ToArray();
        }

        public string CardValues { get
            {
                return String.Join("-", Cards.Select(cm => cm.CardVal));
            } }

    }
}