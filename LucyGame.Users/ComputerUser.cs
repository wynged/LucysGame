using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class ComputerUser : User
    {
        PythonRunner _pyRun;
        public ComputerUser()
        {
            _pyRun = new PythonRunner();
        }

        public override CardChoice ChooseCard(BoardState state)
        {
            string sum = _pyRun.RunScript(3, 4);

            

            return CardChoice.MainDeck;

        }


        public override CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {
            return CardPlacement.V1;
        }

    }
}
