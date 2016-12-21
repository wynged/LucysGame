using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LucysGame.Domain;

namespace LucysGame.User
{
    public static class ComputerUser 
    {

        public static CardChoice ChooseCard(BoardState state)
        {
            string sum = PythonRunner.RunScript(3, 4);

            return CardChoice.MainDeck;

        }


        public static CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {
            return CardPlacement.V1;
        }

    }
}
