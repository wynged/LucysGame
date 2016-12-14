﻿using System;

namespace LucysGame
{
    internal class PlayerModel : ViewModelBase
    {
        public Player Player { get; internal set; }

        public string Name
        {
            get
            {
                return Player.Name;
            }
        }

        public int V1
        {
            get
            {
                return Player.ValV1;
            }
            set
            {
                Player.Cards["V1"] = new Card(value);
                this.SetPropertyChanged("V1");
            }
        }
        public int V2
        {
            get
            {
                return Player.ValV2;
            }
        }
        public int H1
        {
            get
            {
                return Player.ValH1;
            }
            set
            {
                Player.Cards["H1"] = new Card(value);
                this.SetPropertyChanged("H1");
            }
        }
        public int H2
        {
            get
            {
                return Player.ValH2;
            }
        }

        public PlayerModel(Player p)
        {
            Player = p;
        }

        internal void RefreshCards()
        {
            throw new NotImplementedException();
        }
    }
}