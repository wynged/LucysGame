using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame.Domain
{
    public class DecisionResults
    {
        public CardChoice DrawChoice { get; set; }
        public CardPlacement PlacementChoice { get; set; }

        public int DrawnCardValue { get; set; }
        public int HandValueChange { get; set; }
    }
}
