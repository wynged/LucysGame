using System;
using System.Linq;
using System.Collections.ObjectModel;
using LucysGame.Domain;
using JSONUtil;
using System.Threading.Tasks;

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

            StartGameCommand = new ButtonCommand(PlayGame);

            this.AddPlayer("Jack", PlayerType.Human);
            this.AddPlayer("Jill", PlayerType.Human);
            this.AddPlayer("Jane", PlayerType.Human);

            TheBoard.StartGame();
            RefreshUI();
        }



        #region ---Bindable---
        public ButtonCommand StartGameCommand { get; }
        public Board TheBoard { get; internal set; }
        public ButtonCommand ChooseMain { get; }
        public ButtonCommand ChooseDiscard { get; }

        public ObservableCollection<PlayerModel> PlayerModels
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
        public PlayerModel CurrentPlayerModel
        {
            get
            {
                return GetModelOfPlayer(TheBoard.CurrentPlayer);
            }
        }
        public bool CurrentPlayerHuman {
            get
            {
                if (TheBoard.CurrentPlayer.Type == PlayerType.Human)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool _showTotals = false;
        public bool ShowTotals
        {
            get
            {
                return _showTotals;
            }
            set
            {
                if (value != _showTotals)
                {
                    _showTotals = value;
                    SetPropertyChanged("ShowTotals");
                }
            }
        }
        
        private int _turnCount = 0;
        public int TurnCount
        {
            get
            {
                return _turnCount;
            }
            set
            {
                if(value != _turnCount)
                {
                    _turnCount = value;
                    SetPropertyChanged("TurnCount");
                }

            }
        }

        #endregion

        #region ---Behavior---

        public void PlayGame()
        {
            TheBoard.StartGame();
            RefreshUI();

            int maxTurns = 10;
            while (MainDeckCards.Count > 1)
            {
                if (TurnCount > maxTurns)
                {
                    ShowTotals = true;
                    break;
                }
                ProcessCurrentTurn();
            }
        }

        public void ProcessCurrentTurn()
        {
            if(CurrentPlayerHuman)
            {

                Views.DrawChoiceWindow drawChoiceWindow = new Views.DrawChoiceWindow();
                drawChoiceWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                drawChoiceWindow.ShowDialog();

                CardChoice choice = drawChoiceWindow.DrawCard;
                Card card = TheBoard.GetCardFromChoice(choice);

                Views.PlacementChoiceWindow placementWindow = new Views.PlacementChoiceWindow();
                placementWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                
                placementWindow.label_DrawnCard.Content = string.Format("Drawn Value: {0}", card.Number);
                placementWindow.ShowDialog();

                CardPlacement placement = placementWindow.Placement;
                int change = TheBoard.MakePlayerCardPlacement(placement, card);

                RecordMoveandResults(TheBoard.GetBoardStateOfPlayer(TheBoard.CurrentPlayer), choice, card, placement, change);

                EndTurn();
            }
            else
            {
                BoardState state = TheBoard.GetBoardStateOfPlayer(TheBoard.CurrentPlayer);
                PlayerModel pModel = GetModelOfPlayer(TheBoard.CurrentPlayer);

                CardChoice choice = pModel.ComputerChooseCard(state);

                Card card = TheBoard.GetCardFromChoice(choice);

                CardPlacement placement = GetPlacementChoice(pModel, card);

                int change = TheBoard.MakePlayerCardPlacement(placement, card);

                RecordMoveandResults(state, choice, card, placement, change);

                EndTurn();
            }
        }

        private static void RecordMoveandResults(BoardState state, CardChoice choice, Card card, CardPlacement placement, int change)
        {
            DecisionResults results = new DecisionResults();
            results.DrawChoice = choice;
            results.DrawnCardValue = card.Number;
            results.PlacementChoice = placement;
            results.HandValueChange = change;

            JSONwriter.RecordStateAndResults(state, results);
        }


        private static CardPlacement GetPlacementChoice(PlayerModel pModel, Card card)
        {
            return pModel.ComputerCardPlacement(card);
        }

        private void EndTurn()
        {
            TurnCount++;
            TheBoard.GoNextPlayer();
            RefreshUI();
        }

        private PlayerModel GetModelOfPlayer(Player Player)
        {
            PlayerModel PM = PlayerModels.First(pm => pm.Player == Player);
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
            this.SetPropertyChanged("CurrentPlayerHuman");
        }

        public bool ReadyForNextTurn()
        {
            return true;
        }

        public void AddPlayer(string _name, PlayerType type)
        {
            PlayerModels.Add(new PlayerModel(TheBoard.AddPlayer(_name, type)));
        }
        #endregion
    }
}