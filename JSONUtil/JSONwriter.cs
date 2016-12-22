using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LucysGame.Domain;
using System.IO;

namespace JSONUtil
{
    public static class JSONwriter
    {
        public static string JsonStringFromBoardState(BoardState state )
        {
            string stateString = JsonConvert.SerializeObject(state);
            return stateString;
        }

        public static string JsonStringFromResults(DecisionResults results)
        {
            string resultString = JsonConvert.SerializeObject(results);
            return resultString;
        }


        public static bool RecordStateAndResults(BoardState state, DecisionResults results)
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\recordedGameplay\tests.txt", true);
                writer.WriteLine(JsonStringFromBoardState(state));
                writer.WriteLine(JsonStringFromResults(results));
                writer.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
