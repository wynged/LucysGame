using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class HumanUser : User
    {
        public override CardChoice ChooseCard(BoardState state)
        {
            throw new NotImplementedException();
        }

        public override CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {
            throw new NotImplementedException();
        }
    }
}
