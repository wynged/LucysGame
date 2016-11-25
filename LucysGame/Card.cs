using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    public class Card
    {
        public int Number { get; }
        public char Suit { get; }

        public Card(int _num, char _suit)
        {
            Number = _num;
            Suit = _suit;
        }

        public Card(int _num)
        {
            Number = _num;
        }


    }
}
