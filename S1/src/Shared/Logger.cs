using System;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Shared
{
    /// <summary>
    /// Class for logging messages and errors to a logfile.
    /// </summary>
    public static class Logger
    {
        private static log4net.ILog logger;

        /// <summary>
        /// Gets if the logger has been initialized and is ready for use.
        /// </summary>
        public static bool IsInitialized { get; set; }

        /// <summary>
        /// Initializes the logger. This is requires before any logging can be done.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the initialization fails</exception>
        public static void Initialize()
        {
            try
            {
                string dir = FilePathUtility.GetFullPath(Assembly.GetExecutingAssembly());
                string path = Path.Combine(dir, "log4net.config");
                using (Stream s = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    log4net.Config.XmlConfigurator.Configure(s);
                }

                logger = log4net.LogManager.GetLogger("File");

                IsInitialized = true;
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Failed initializing logging facility. Because of its current configuration (or lack thereof)", e);
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// Depending on the logging threshold the message might be supressed.
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void LogDebug(string message)
        {
            if (IsInitialized)
            {
                logger.Debug(message);
            }
            else
            {
                Trace.WriteLine("Debug: " + message);
            }
        }

        /// <summary>
        /// Logs a info message.
        /// Depending on the logging threshold the message might be supressed.
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void LogInfo(string message)
        {
            if (IsInitialized)
            {
                logger.Info(message);
            }
            else
            {
                Trace.WriteLine("Info: " + message);
            }
        }

        /// <summary>
        /// Logs a warning message.
        /// Depending on the logging threshold the message might be supressed.
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void LogWarning(string message)
        {
            if (IsInitialized)
            {
                logger.Warn(message);
            }
            else
            {
                Trace.WriteLine("Warn: " + message);
            }
        }

        /// <summary>
        /// Logs an error message along with an exception.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="e">The exception to log</param>
        public static void LogError(string message, Exception e)
        {
            if (IsInitialized)
            {
                logger.Error(message, e);
            }
            else
            {
                Trace.WriteLine("Error: " + message + "Exception: " + e + "Message: " + e.Message);
            }
        }
    }
}
