using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class HumanUser : User
    {
        public override CardChoice ChooseCard(List<int> _playerCardValues, int _discardCardValue)
        {
            throw new NotImplementedException();
        }

        public override CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {
            throw new NotImplementedException();
        }
    }
}
