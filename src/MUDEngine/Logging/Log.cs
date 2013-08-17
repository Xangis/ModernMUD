using System;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Provides functionality to perform logging by file, console or event log.
    /// </summary>
    /// <remarks>
    /// Abstracts the logging details so that only static methods need to be called for
    /// all users in the system can log. The log details are configured in the application
    /// configuration file.
    /// </remarks>
    public sealed class Log
    {
        private static bool _showTrace = false;
        private static bool _showExceptionDetails = false;
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// Determines if exceptions are enabled.
        /// </summary>
        public static bool ExceptionDetailsEnabled
        {
            get
            {
                // Show exception details if traces are enabled
                return _showExceptionDetails || _showTrace;
            }
            set { _showExceptionDetails = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether trace logging is enabled.
        /// </summary>
        public static bool TraceEnabled
        {
            get { return _showTrace; }
            set { _showTrace = value; }
        }

        /// <summary>
        /// Logs the error message to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        public static void Error(string message)
        {
            // Call override.
            Error(SocketConnection.RemoveANSICodes(message), string.Empty);
        }

        /// <summary>
        /// Logs the error message to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(string format, params object[] args)
        {
            // Call override.
            Error(string.Format(format, args));
        }

        /// <summary>
        /// Logs the error message and exception information to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        /// <param name="e">The exception to log.</param>
        public static void Error(string message, Exception e)
        {
            // Call override.
            Error(message, e, string.Empty);
        }

        /// <summary>
        /// Logs the error message and exception information to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="e">The exception to log.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(string format, Exception e, params object[] args)
        {
            // Call override.
            Error(string.Format(format, args), e, string.Empty);
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        /// <param name="name">The name for the message.</param>
        public static void Error(string message, string name)
        {
            lock (_syncRoot)
            {
                // log message.
                LogEntry le = new LogEntry(message, name);
                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_DEBUG, Limits.LEVEL_OVERLORD, message );
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="name">The name for the message.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(string format, string name, params object[] args)
        {
            lock (_syncRoot)
            {
                // log message.
                LogEntry le = new LogEntry(string.Format(format, args), name);
                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Error(string type, string subType, string name, string message)
        {
            lock (_syncRoot)
            {
                LogEntry le = new LogEntry(type, subType, name, message);
                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="args">The arguments.</param>
        /// <param name="format">The format.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Error(string type, string subType, string name, string format, params object[] args)
        {
            lock (_syncRoot)
            {
                LogEntry le = new LogEntry(type, subType, name, string.Format(format, args));
                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="e">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Error(string type, string subType, string name, string message, Exception e)
        {
            lock (_syncRoot)
            {
                LogEntry le;
                le = new LogEntry(type, subType, name, message, e);
                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="args">The arguments.</param>
        /// <param name="e">The exception.</param>
        /// <param name="format">The format.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Error(string type, string subType, string name, string format, Exception e, params object[] args)
        {
            lock (_syncRoot)
            {
                LogEntry le;
                if (_showExceptionDetails)
                    le = new LogEntry(type, subType, name, string.Format(format, args), e);
                else
                    le = new LogEntry(type, subType, name, string.Format(format, args));

                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message and exception information to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        /// <param name="e">The exception to log.</param>
        /// <param name="name">The name for the message.</param>
        public static void Error(string message, Exception e, string name)
        {
            lock (_syncRoot)
            {
                LogEntry le;
                if (message == null)
                    message = string.Empty;

                // Set message.
                if (e != null)
                {
                    if (ExceptionDetailsEnabled)
                        le = new LogEntry(message, name, e);
                    else
                    {
                        // Get a detailed message.
                        Exception e1 = e;
                        if (e1 is GenericException)
                        {
                            message += " | " + e1.Message;
                            while (e1.InnerException != null && e1.InnerException is GenericException)
                            {
                                e1 = e1.InnerException;
                                message += " | " + e1.Message;
                            }
                        }
                        le = new LogEntry(message, name);
                    }
                }
                else
                    le = new LogEntry(message, name);

                // Log message.
                le.EventType = LogEntry.LogType.Error;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message and exception information to the source with the specified header.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="e">The exception to log.</param>
        /// <param name="name">The name for the message.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(string format, Exception e, string name, params object[] args)
        {
            Error(string.Format(format, args), e, name);
        }

        /// <summary>
        /// Logs the info message to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        public static void Info(string message)
        {
            // Call override.
            Info(SocketConnection.RemoveANSICodes(message), string.Empty);
        }

        /// <summary>
        /// Logs the info message to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="args">The arguments.</param>
        public static void Info(string format, params object[] args)
        {
            // Call override.
            Info(string.Format(format, args), string.Empty);
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        /// <param name="name">The name for the message.</param>
        public static void Info(string message, string name)
        {
            lock (_syncRoot)
            {
                // log message.
                LogEntry le = new LogEntry(message, name);
                le.EventType = LogEntry.LogType.Info;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="name">The name for the message.</param>
        /// <param name="args">The arguments.</param>
        public static void Info(string format, string name, params object[] args)
        {
            lock (_syncRoot)
            {
                // log message.
                LogEntry le = new LogEntry(string.Format(format, args), name);
                le.EventType = LogEntry.LogType.Info;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Info(string type, string subType, string name, string message)
        {
            lock (_syncRoot)
            {
                LogEntry le = new LogEntry(type, subType, name, message);
                le.EventType = LogEntry.LogType.Info;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the error message to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="args">The type.</param>
        /// <param name="format">The format.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Info(string type, string subType, string name, string format, params object[] args)
        {
            lock (_syncRoot)
            {
                LogEntry le = new LogEntry(type, subType, name, string.Format(format, args));
                le.EventType = LogEntry.LogType.Info;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the trace information to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        public static void Trace(string message)
        {
            // call override.
            Trace(SocketConnection.RemoveANSICodes(message), string.Empty);
        }

        /// <summary>
        /// Logs the trace information to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="args">The arguments.</param>
        public static void Trace(string format, params object[] args)
        {
            // call override.
            Trace(string.Format(format, args), string.Empty);
        }

        /// <summary>
        /// Logs the trace information to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Trace(string type, string subType, string name, string message)
        {
            lock (_syncRoot)
            {
                LogEntry le = new LogEntry(type, subType, name, message);
                le.EventType = LogEntry.LogType.Trace;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the trace information to the source.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="args">The arguments.</param>
        /// <param name="format">The format.</param>
        /// <param name="name">The name.</param>
        /// <param name="subType">The sub type.</param>
        /// <param name="type">The type.</param>
        public static void Trace(string type, string subType, string name, string format, params object[] args)
        {
            lock (_syncRoot)
            {
                LogEntry le = new LogEntry(type, subType, name, string.Format(format, args));
                le.EventType = LogEntry.LogType.Trace;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the trace information to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="message">The message to log.</param>
        /// <param name="name">The name for the message.</param>
        public static void Trace(string message, string name)
        {
            lock (typeof(Log))
            {
                // log message.
                LogEntry le = new LogEntry(message, name);
                le.EventType = LogEntry.LogType.Trace;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Logs the trace information to the source with the specified _name.
        /// </summary>
        /// <remarks>
        /// See summary.
        /// </remarks>
        /// <param name="format">The format to log.</param>
        /// <param name="name">The name for the message.</param>
        /// <param name="args">The arguments.</param>
        public static void Trace(string format, string name, params object[] args)
        {
            lock (typeof(Log))
            {
                // Log message.
                LogEntry le = new LogEntry(string.Format(format, args), name);
                le.EventType = LogEntry.LogType.Trace;
                WriteLogLine(le);
            }
        }

        /// <summary>
        /// Substitute _function for System.Diagnostics.Trace because that doesn't work for whatever reason.
        /// </summary>
        /// <param name="entry"></param>
        public static void WriteLogLine(LogEntry entry)
        {
            FileStream stream = new FileStream("b3log.txt", FileMode.Append, FileAccess.Write, FileShare.None );
            StreamWriter sw = new StreamWriter(stream);
            string logLine = DateTime.Now.ToLongTimeString() + " " + entry.EventType + ": " + entry.Message;
            sw.WriteLine(logLine);
            sw.Flush();
            sw.Close();
            Console.WriteLine(logLine);
        }
    }

}
