using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LucysGame
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
                this.SetPropertyChanged("Players");
            }
        }
        public string NumCardsInDeck
        {
            get
            {
                return TheBoard.MainDeck.Count.ToString();
            }
        }
        public string CurrentPlayerModelName
        {
            get
            {
                return Players.Where(x => x.Player == TheBoard.CurrentPlayer).First().Name;
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
                    this.SetPropertyChanged("MainDeckCards");
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

        public void NextTurn()
        {
            Player changePlayer = TheBoard.GoNextPlayer();
            PlayerModel changePM = GetModelOfPlayer(changePlayer);
            CardChoice choice = TheBoard.CurrentPlayer.PlayerCardChoice(TheBoard.TopOfDiscardPile);
            Card card = TheBoard.GetCardFromChoice(choice);
            CardPlacement placement = TheBoard.CurrentPlayer.PlayerCardPlacement(card);
            TheBoard.MakePlayerCardPlacement(placement, card);
            RefreshCards();
            this.SetPropertyChanged("CurrentPlayerModelName");
            this.SetPropertyChanged("MainDeckCardString");
        }

        private PlayerModel GetModelOfPlayer(Player changePlayer)
        {
            PlayerModel PM = Players.First(pm => pm.Player == changePlayer);
            return PM;
        }

        private void RefreshCards()
        {
            MainDeckCards = new ObservableCollection<Card>( TheBoard.MainDeck);
            _players.Clear();
            foreach (Player p in TheBoard.Players)
            {
                _players.Add(new PlayerModel(p));
            } 
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