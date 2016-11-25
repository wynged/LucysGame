namespace LucysGame
{
    internal class PlayerModel
    {
        public string Name
        {
            get
            {
                return Player.Name;
            }
        }

        public int V1 { get
            {
                return Player.ValV1;
            }
        }
        public int V2 { get
            {
                return Player.ValV2;
            }
        }
        public int H1 { get
            {
                return Player.ValH1;
            }
        }
        public int H2 { get
            {
                return Player.ValH2;
            }
        }

        public Player Player { get; internal set; }

        public PlayerModel(Player p)
        {
            Player = p;
        }

    }
}