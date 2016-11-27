using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    public abstract class User
    {
        abstract public CardChoice ChooseCard(List<int> _playerCardValues, int _discardCardValue);

        abstract public CardPlacement PlaceCard(List<int> _playerCards, int _cardToPlace);
    }

    public enum CardChoice { Discard, MainDeck}

    public enum CardPlacement { V1, V2, H1, H2, Discard }
}
