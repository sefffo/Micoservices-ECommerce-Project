using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.SharedLibirary.Logs
{
    public static class LogExceptions  // Static class to hold log exception methods to end the use of duplicate code and dependency injection
    {

        public static void LogException(Exception ex)
        {
            LogToFile(ex.Message);
            LogToConsole(ex.Message);
            LogToDebug(ex.Message);
        }

        public static void LogToFile(string message)
        {
            // Code to log message to a file
            Log.Information(message);
        }
        public static void LogToConsole(string message)
        {
            // Code to log message to the console
            Log.Warning($"Logging to console: {message}");
        }
        public static void LogToDebug(string message)
        {
            // Code to log message to debug output
            Log.Debug($"Logging to debug: {message}");
        }
    }
}
