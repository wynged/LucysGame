using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class ComputerUser : User
    {
        public override CardChoice ChooseCard(List<int> _playerCardValues, int _discardCardValue)
        {
            return CardChoice.MainDeck;
        }


        public override CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {
            return CardPlacement.H1;
        }

    }
}
