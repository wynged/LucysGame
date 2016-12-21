using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LucysGame.Domain;

namespace JSONUtil
{
    public static class JSONwriter
    {
        public static void JsonStringFromBoardState(BoardState state )
        {
            string stateString = JsonConvert.SerializeObject(state);

        }
    }
}
