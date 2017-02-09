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
                string basepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + String.Format("\\Documents\\_code\\_gitHub\\LucysGame\\recordedGameplay\\");
                string filepath = String.Format("{0} play.txt", System.DateTime.Now.ToString("yyyymmddHHmm"));
                string fullpath = basepath + filepath;

                using (StreamWriter writer = File.AppendText(fullpath))
                {
                    writer.WriteLine(JsonStringFromBoardState(state));
                    writer.WriteLine(JsonStringFromResults(results));
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool RecordScoreString(string score)
        {
            string scoresPath = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\recordedGameplay\oldPlays\_scores.txt";
            using (StreamWriter writer = File.AppendText(scoresPath))
            {
                writer.WriteLine(score);
            }

            return true;
        }
    }
}
