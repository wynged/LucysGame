using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame.ViewModels
{
    class CardModel
    {
        Card _card;
        public CardModel(Card c)
        {
            _card = c;
        }

        public string CardVal {
            get
            {
                if (_card == null) return "?";
                if(_card.Visibility == CardVisibilityState.Public)
                {
                    return _card.Number.ToString();
                }
                else
                {
                    return "H";
                }
            }
        }
    }
}
