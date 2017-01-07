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

            NextTurnCommand = new ButtonCommand(TakeTurns);//, ReadyForNextTurn);
            ChooseMain = new ButtonCommand(() => MakeChoice(CardChoice.MainDeck));
            ChooseDiscard = new ButtonCommand(() =>
            {
                HumanChoice = CardChoice.Discard;
                NeedToMakeChoice = false;
            });

            this.AddPlayer("Jack", PlayerType.Human);
            this.AddPlayer("Jill", PlayerType.Deep);
            this.AddPlayer("Jane", PlayerType.Deep);

            TheBoard.StartGame();
            RefreshUI();
        }



        #region ---Bindable---
        public ButtonCommand NextTurnCommand { get; }
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

        public CardChoice HumanChoice { get; private set; }
        private CardChoice _cardChoice;

        private bool _needToChoose;
        public bool NeedToMakeChoice
        {
            get
            {
                return _needToChoose;
            }
            private set
            {
                if ((value != _needToChoose))
                {
                    _needToChoose = value;
                    SetPropertyChanged("NeedToMakeChoice");
                }
            }
        }

        #endregion

        #region ---Behavior---

        public void TakeTurns()
        {
            for (int i=0; i<10; i++)
            {
                ProcessCurrentTurn();
            }
        }

        public void ProcessCurrentTurn()
        {
            if(CurrentPlayerHuman)
            {
                LucysGame.Views.DrawChoiceWindow drawChoiceWindow = new Views.DrawChoiceWindow();

                drawChoiceWindow.ShowDialog();

                if (NeedToMakeChoice == false)
                {
                    Card card = TheBoard.GetCardFromChoice(HumanChoice);
                }
                    
                ResetChoices();
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

                DecisionResults results = new DecisionResults();
                results.DrawChoice = choice;
                results.DrawnCardValue = card.Number;
                results.PlacementChoice = placement;
                results.HandValueChange = change;

                JSONUtil.JSONwriter.RecordStateAndResults(state, results);

                EndTurn();
            }
        }

        private void MakeChoice(CardChoice choice)
        {
            HumanChoice = choice;
            NeedToMakeChoice = false;
        }

        private void ResetChoices()
        {
            NeedToMakeChoice = true;
        }

        private static CardPlacement GetPlacementChoice(PlayerModel pModel, Card card)
        {
            return pModel.ComputerCardPlacement(card);
        }

        private void EndTurn()
        {
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