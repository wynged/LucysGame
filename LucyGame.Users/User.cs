using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LucysGame.Domain;

namespace LucysGame.User
{
    public  static class User
    {
        public static CardChoice ChooseCard(BoardState state) { return CardChoice.Discard; }// List<int> _playerCardValues, int _discardCardValue);

        public static CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace) { return CardPlacement.Discard; }
    }


}
