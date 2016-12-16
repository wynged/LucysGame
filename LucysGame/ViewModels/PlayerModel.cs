using System;
using System.Linq;

namespace LucysGame.ViewModels
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

        //public int V1
        //{
        //    get
        //    {
        //        return Player.ValV1;
        //    }
        //    set
        //    {
        //        Player.CardDict["V1"] = new Card(value);
        //        this.SetPropertyChanged("V1");
        //    }
        //}
        //public int V2
        //{
        //    get
        //    {
        //        return Player.ValV2;
        //    }
        //}
        //public int H1
        //{
        //    get
        //    {
        //        return Player.ValH1;
        //    }
        //    set
        //    {
        //        Player.CardDict["H1"] = new Card(value);
        //        this.SetPropertyChanged("H1");
        //    }
        //}
        //public int H2
        //{
        //    get
        //    {
        //        return Player.ValH2;
        //    }
        //}

        CardModel[] Cards;

        public PlayerModel(Player p)
        {
            Player = p;
            Cards = p.CardDict.Values.Select(c => new CardModel(c)).ToArray();
        }

        public string CardValues { get
            {
                return String.Join("-", Cards.Select(cm => cm.CardVal));
            } }

    }
}