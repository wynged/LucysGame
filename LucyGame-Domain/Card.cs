using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame.Domain
{
    public class Card
    {
        public int Number { get; set;        }
        public CardVisibilityState Visibility { get; set;}


        public Card(int num)
        {
            Number = num;
            Visibility = CardVisibilityState.Hidden;
        }


    }
}
