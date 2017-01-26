using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LucysGame.Domain;


namespace LucysGame.Users
{
    public static class DeepNNUser
    {
        public static CardChoice ChooseCard(BoardState state)
        {
            string choiceSstring = PythonRunner.ChooseDrawCard(state.DiscardPile.Last().Number);
            int choiceInt = int.Parse(choiceSstring);

            CardChoice choice= (CardChoice)Enum.ToObject(typeof(CardChoice), choiceInt);

            return choice;
        }

        public static CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {
            return CardPlacement.Discard;
        } 
    }
}
