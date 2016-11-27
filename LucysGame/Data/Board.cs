using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucysGame
{
    class Board
    {
        public List<Player> Players;
        public List<Card> MainDeck { get; internal set; }
        public Player CurrentPlayer { get; internal set; }
        public List<Card> DiscardPile;

        public Board()
        {
            DiscardPile = new List<Card>();
            Players = new List<Player>();
        }

        internal Player AddPlayer(string v)
        {
            Player newPlayer = new Player(v);
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

        private void DiscardCard(Card card)
        {
            DiscardPile.Add(card);
        }

        private void DealCards()
        {
            foreach (Player p in Players)
            {
                foreach (string key in new List<string>(p.Cards.Keys))
                {
                    p.Cards[key] = MainDeck.Take(1).First();
                    MainDeck.RemoveAt(0);
                }
            }
        }

        internal void NextPlayerAction()
        {
            NextPlayer();
            PlayerTurn();
        }

        private void PlayerTurn()
        {
            CardChoice choice = CurrentPlayer.PlayerCardChoice(DiscardPile.Last());
            Card newCard = GetCardFromChoice(choice);

            CardPlacement placement = CurrentPlayer.PlayerCardPlacement(newCard);
            MakePlayerCardPlacement(placement, newCard);
        }

        private void MakePlayerCardPlacement(CardPlacement placement, Card newCard)
        {
            switch (placement) {
                case CardPlacement.H1:
                    DiscardCard(CurrentPlayer.SwapCard("H1", newCard));
                    break;
               
            }
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

        private Card GetCardFromChoice(CardChoice choice)
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

        private void NextPlayer()
        {
            int i = Players.IndexOf(CurrentPlayer);
            if (i == Players.Count - 1)
            {
                CurrentPlayer = Players[0];
            }
            else
            {
                CurrentPlayer = Players[i + 1];
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
