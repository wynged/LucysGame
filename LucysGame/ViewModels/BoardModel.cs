using System;
using System.Linq;
using System.Collections.ObjectModel;
using LucysGame.Domain;
using JSONUtil;

namespace LucysGame.ViewModels
{
    class BoardModel : ViewModelBase
    {
        private ObservableCollection<PlayerModel> _players;
        private ObservableCollection<Card> _mainDeckCards;

        public BoardModel()
        {
            TheBoard = new Board();

            _players = new ObservableCollection<PlayerModel>();

            NextTurnCommand = new ButtonCommand(NextTurn, ReadyForNextTurn);

            this.AddPlayer("Jack");
            this.AddPlayer("Jill");
            this.AddPlayer("Jane");

            TheBoard.StartGame();
            RefreshUI();
        }

        public ButtonCommand NextTurnCommand { get; }
        public Board TheBoard { get; internal set; }

        public ObservableCollection<PlayerModel> Players
        {
            get
            {
                return _players;

            }
            private set {
            }
        }
        public string NumCardsInDeck
        {
            get
            {
                return TheBoard.MainDeck.Count.ToString();
            }
        }
        public string NextPlayerName
        {
            get
            {
                return TheBoard.GetNextPlayer().Name;
            }

        }
        public ObservableCollection<Card> MainDeckCards
        {
            get
            {
                return new ObservableCollection<Card>(TheBoard.MainDeck);
            }
            set
            {
                if (value != _mainDeckCards)
                {
                    _mainDeckCards = value;
                }
            }
        }
        public string MainDeckCardString
        {
            get
            {
                return String.Join("\n", TheBoard.MainDeck.Select(x => x.Number.ToString()));
            }
        }
        public ObservableCollection<Card> DiscardPile
        {
            get
            {
                return new ObservableCollection<Card>(TheBoard.DiscardPile);
            }
        }

        public void NextTurn()
        {
            TheBoard.GoNextPlayer();

            BoardState state = TheBoard.GetBoardStateOfPlayer(TheBoard.CurrentPlayer);

            CardChoice choice = GetModelOfPlayer(TheBoard.CurrentPlayer).PlayerCardChoice(TheBoard.GetBoardStateOfPlayer(TheBoard.CurrentPlayer));
            Card card = TheBoard.GetCardFromChoice(choice);
            CardPlacement placement = GetModelOfPlayer(TheBoard.CurrentPlayer).PlayerCardPlacement(card);
            int change = TheBoard.MakePlayerCardPlacement(placement, card);

            DecisionResults results = new DecisionResults();
            results.DrawChoice = choice;
            results.DrawnCardValue = card.Number;
            results.PlacementChoice = placement;
            results.HandValueChange = change;



            JSONUtil.JSONwriter.RecordStateAndResults(state, results);

            RefreshUI();
        }

        private PlayerModel GetModelOfPlayer(Player Player)
        {
            PlayerModel PM = Players.First(pm => pm.Player == Player);
            return PM;
        }

        private void RefreshUI()
        {
            _players.Clear();
            foreach (Player p in TheBoard.Players)
            {
                _players.Add(new PlayerModel(p));
            }
            this.SetPropertyChanged("MainDeckCards");
            this.SetPropertyChanged("DiscardPile");
            this.SetPropertyChanged("NextPlayerName");
        }

        public bool ReadyForNextTurn()
        {
            return true;
        }

        public void AddPlayer(string _name)
        {
            Players.Add(new PlayerModel(TheBoard.AddPlayer(_name)));
        }
    }
}