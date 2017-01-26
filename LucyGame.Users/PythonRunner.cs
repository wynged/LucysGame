using System.Diagnostics;
using System.IO;


namespace LucysGame.Users
{
    internal static class PythonRunner
    {
        internal static string ChooseDrawCard(int x)
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
    }
}