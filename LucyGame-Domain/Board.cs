using System;
using System.Collections.Generic;
using System.Linq;

namespace LucysGame.Domain
{
    public class Board
    {
        public List<Player> Players;
        public List<Card> MainDeck { get; internal set; }
        public Player CurrentPlayer { get; internal set; }
        public List<Card> DiscardPile;
        public Card TopOfDiscardPile
        {
            get
            {
                return DiscardPile.Last();
            }
        }

        public Board()
        {
            DiscardPile = new List<Card>();
            Players = new List<Player>();
        }

        public BoardState GetBoardStateOfPlayer(Player p)
        {
            BoardState state = new BoardState();
            state.PlayerCards = CurrentPlayer.CardDict.Values.ToArray();
            state.DiscardPile = DiscardPile.ToArray();
            state.PlayerType = CurrentPlayer.Type;

            return state;
        }

        public Player AddPlayer(string v, PlayerType playerType)
        {
            Player newPlayer = new Player(v);
            newPlayer.Type = playerType;
            Players.Add(newPlayer);
            return newPlayer;
        }

        public List<Card> InitializeDeck()
        {
            List<char> suits = new List<char> { 'S', 'C', 'H', 'D' };
            List<int> numbers = Enumerable.Range(1, 12).ToList();

            List<Card> cards = numbers.SelectMany(x => new List<Card> { new Card(x),
                                                                        new Card(x),
                                                                        new Card(x),
                                                                        new Card(x)}).ToList();
            //add jokers
            cards.AddRange(new List<Card> { new Card(0), new Card(0) });
            cards.Shuffle();
            MainDeck = cards;
            return cards;
        }

        public void StartGame()
        {
            MainDeck = InitializeDeck();
            DealCards();
            CurrentPlayer = Players[0];
            DiscardCard(TakeTopOfMainDeck());
        }

        private void DealCards()
        {
            foreach (Player p in Players)
            {
                foreach (string key in new List<string>(p.CardDict.Keys))
                {
                    p.CardDict[key] = MainDeck.Take(1).First();
                    if (key.Contains("V"))
                    {
                        p.CardDict[key].Visibility = CardVisibilityState.Public;
                    }
                    MainDeck.RemoveAt(0);
                }
            }
        }

        internal void DiscardCard(Card card)
        {
            card.Visibility = CardVisibilityState.Public;
            DiscardPile.Add(card);
        }

        public void GoNextPlayer()
        {
            Player next = GetNextPlayer();
            CurrentPlayer = next;
        }

        public int MakePlayerCardPlacement(CardPlacement placement, Card newCard)
        {
            switch (placement) {
                case CardPlacement.H1:
                    {
                        int oldVal = CurrentPlayer.CardDict["H1"].Number;
                        DiscardCard(CurrentPlayer.SwapCard("H1", newCard));
                        return newCard.Number - oldVal;
                    }
                case CardPlacement.H2:
                    {
                        int oldVal = CurrentPlayer.CardDict["H2"].Number;
                        DiscardCard(CurrentPlayer.SwapCard("H2", newCard));
                        return newCard.Number - oldVal;
                    }
                case CardPlacement.V1:
                    {
                        int oldVal = CurrentPlayer.CardDict["V1"].Number;
                        DiscardCard(CurrentPlayer.SwapCard("V1", newCard));
                        return newCard.Number - oldVal;
                    }
                case CardPlacement.V2:
                    {
                        int oldVal = CurrentPlayer.CardDict["V2"].Number;
                        DiscardCard(CurrentPlayer.SwapCard("V2", newCard));
                        return newCard.Number - oldVal;
                    }
                case CardPlacement.Discard:
                    {
                        DiscardCard(newCard);
                        return 0;
                    }
            }
            return 0;
        }

        public Card GetCardFromChoice(CardChoice choice)
        {
            if (choice == CardChoice.Discard)
            {
                return TakeTopOfDiscardPile();
            }
            else
            {
                return TakeTopOfMainDeck();
            }
        }

        private Card TakeTopOfMainDeck()
        {
            Card c = MainDeck[0];
            MainDeck.RemoveAt(0);
            return c;
        }

        private Card TakeTopOfDiscardPile()
        {
            if (DiscardPile.Count < 1)
            {
                return null;
            }
            else
            {
                Card c = DiscardPile.Last();
                DiscardPile.Remove(c);
                return c;
            }
        }

        public Player GetNextPlayer()
        {
            int i = Players.IndexOf(CurrentPlayer);
            if (i == Players.Count - 1)
            {
                return Players[0];
            }
            else
            {
                return Players[i + 1];
            }

        }
    }

    static class Extensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
