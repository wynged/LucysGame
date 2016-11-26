using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame.Data
{
    public abstract class UserBase
    {
        abstract public CardChoice ChooseCard(List<Card> _playerCards, Card _discardCard);

        abstract public CardPlacement PlaceCard(Player _player);
    }


    public enum CardChoice { Discard, MainDeck}

    public enum CardPlacement { V1, V2, H1, H2, Discard }
}
