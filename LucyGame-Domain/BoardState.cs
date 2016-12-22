using Newtonsoft.Json;

namespace LucysGame.Domain
{
    [JsonObject ]
    public class BoardState 
    {
        public Card[] PlayerCards { get;  set; }
        public Card[] DiscardPile { get;  set; }
        
    }
}