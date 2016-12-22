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
            Random rand = new Random();
            string[] choices = Enum.GetNames(typeof(CardChoice));
            int i = rand.Next(choices.Length);

            CardChoice thisChoice =(CardChoice) Enum.ToObject(typeof(CardChoice), i);

            return  thisChoice;

        }


        public static CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace)
        {

            Random rand = new Random();
            string[] choices = Enum.GetNames(typeof(CardPlacement));
            int i = rand.Next(choices.Length);

            CardPlacement thisChoice =(CardPlacement) Enum.ToObject(typeof(CardPlacement), i);

            return  thisChoice;
        }

    }
}
