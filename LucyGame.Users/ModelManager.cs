using System.Diagnostics;
using System.IO;
using System.Collections.Generic;


namespace LucysGame.Users
{
    public static class ModelManager
    {
        public static string ChooseDrawCard(int x)
        {
            // full path of python interpreter  
            string python = @"C:\Python27\python.exe";

            // python app to call  
            string pythonApp = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\LucyLearner\Get_Draw_Choice.py";
            string simpleApp = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\LucyLearner\simplePy.py";
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = true;
            myProcessStartInfo.CreateNoWindow = true;

            // start python app with 3 arguments  
            // 1st argument is pointer to itself, 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = pythonApp + " " + x;// + " " + 4;

            Process myProcess = new Process();
            myProcess.EnableRaisingEvents = true;
            
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            StreamReader myStreamReader = myProcess.StandardOutput;
            string lastLine = "";
            while(myStreamReader.EndOfStream == false)
            {
                lastLine = myStreamReader.ReadLine();
            }

            

            StreamReader errorReader = myProcess.StandardError;
            
            string myerror = errorReader.ReadToEnd();
            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();

            return lastLine;
        }

        public static string ChooseCardPlacement(List<int> vals)
        {
            // full path of python interpreter  
            string python = @"C:\Python27\python.exe";

            // python app to call  
            string pythonApp = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\LucyLearner\Get_Card_Placement.py";
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = true;
            myProcessStartInfo.CreateNoWindow = true;

            // start python app with 3 arguments  
            // 1st argument is pointer to itself, 2nd and 3rd are actual arguments we want to send 
            string pyArg = "[" + string.Join(",", vals) + "]";
            myProcessStartInfo.Arguments = pythonApp + " " + pyArg;

            Process myProcess = new Process();
            myProcess.EnableRaisingEvents = true;

            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            StreamReader myStreamReader = myProcess.StandardOutput;
            string lastLine = "";
            while (myStreamReader.EndOfStream == false)
            {
                lastLine = myStreamReader.ReadLine();
            }

            StreamReader errorReader = myProcess.StandardError;

            string myerror = errorReader.ReadToEnd();
            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();

            return lastLine;
        }

        public static bool ReTrainDrawChoice()
        {
            // full path of python interpreter  
            string python = @"C:\Python27\python.exe";

            // python app to call  
            string pythonApp = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\LucyLearner\TrainNN_DrawChoice.py";
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = true;
            
            // start python app with 3 arguments  
            // 1st argument is pointer to itself, next are actual arguments we want to send 
            myProcessStartInfo.Arguments = pythonApp;

            Process myProcess = new Process();
            myProcess.EnableRaisingEvents = true;

            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();

            StreamReader errorRead = myProcess.StandardError;
            string myError = errorRead.ReadToEnd();

            StreamReader outputRead = myProcess.StandardOutput;
            string myOutput = outputRead.ReadToEnd();

            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();

            return true;
        }


        public static bool ReTrainCardPlacement()
        {
            // full path of python interpreter  
            string python = @"C:\Python27\python.exe";

            // python app to call  
            string pythonApp = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\LucyLearner\TrainNN_PlaceCard.py";
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = true;
            
            // start python app with 3 arguments  
            // 1st argument is pointer to itself, next are actual arguments we want to send 
            myProcessStartInfo.Arguments = pythonApp;

            Process myProcess = new Process();
            myProcess.EnableRaisingEvents = true;

            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();

            StreamReader errorRead = myProcess.StandardError;
            string myError = errorRead.ReadToEnd();

            StreamReader outputRead = myProcess.StandardOutput;
            string myOutput = outputRead.ReadToEnd();

            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();

            return true;
        }

        public static bool MoveTrainedGameplay()
        {
            string gamePlayPath = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\recordedGameplay\";
            string archivePath = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\recordedGameplay\oldPlays\";

            foreach (string aFile in Directory.EnumerateFiles(gamePlayPath))
            {
                if (aFile.EndsWith(".txt"))
                {
                    string[] filePathSplit = aFile.Split('\\');
                    string fileName = filePathSplit[filePathSplit.Length - 1];
                    if (File.Exists(archivePath + fileName))
                    {
                        System.Random random = new System.Random();
                        fileName = fileName + random.Next().ToString() + ".txt";
                    }
                    File.Move(aFile, archivePath+fileName);
                }
            }

            return true;
        }
    }
}