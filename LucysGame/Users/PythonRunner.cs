using System.Diagnostics;
using System.IO;

namespace LucysGame
{
    internal class PythonRunner
    {
        private string python;
        private string myPythonApp;

        public PythonRunner()
        {

            // full path of python interpreter  
            python = @"C:\Python27\python.exe";

            // python app to call  
            myPythonApp = @"C:\Users\erudisaile\Documents\_code\_gitHub\LucysGame\py\simplePy.py";

        }

        internal string RunScript(int x, int y)
        {
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st argument is pointer to itself, 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = myPythonApp + " " + x + " " + y;

            Process myProcess = new Process();
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();

            return myString;

        }
    }
}