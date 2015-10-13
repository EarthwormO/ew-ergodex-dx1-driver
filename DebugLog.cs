using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;

namespace DX1Utility
{
    public sealed class DebugLog
    {
        //Singleton class so all threads write to the same file

        private static StreamWriter logFile;
        private static readonly DebugLog instance = new DebugLog();

        private DebugLog() 
        {
            if (!Directory.Exists(Path.GetDirectoryName(Globals.debugLogFile)))
            {
                //The Folder attempting to be written to does not exist.  Don't attempt to write
                Globals.debugLog = false;
                return;
            }
            logFile = new StreamWriter(Globals.debugLogFile, false);
        }

        public static DebugLog Instance
        {
            get
            {
                return instance;
            }
        }

        public void writeLog(string tempString, bool timeStamp = false)
        {
            //If not logging, immediately exit
            if (!Globals.debugLog) return;

            if (timeStamp)
            {
                tempString = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": " + tempString; 
                //tempString = DateTime.Now.ToString() + ": " + tempString;
            }

            logFile.WriteLine(tempString);
        }

        public void closeFile()
        {
            logFile.Close();
        }

    }
}
