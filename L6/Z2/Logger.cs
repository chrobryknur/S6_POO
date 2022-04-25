using System;
using System.IO;

namespace Logger
{
    public interface ILogger
    {
        void Log( string Message );
    }
    public enum LogType { None, Console, File }
    public class LoggerFactory
    {
        public ILogger GetLogger( LogType LogType, string? Parameters = null )
        {
            switch (LogType)
            {
                case LogType.None:
                {
                    return new NullLogger();
                }
                case LogType.Console:
                {
                    return new ConsoleLogger();
                }
                case LogType.File:
                {
                    if( Parameters != null )
                    {
                        return new FileLogger(Parameters);
                    }
                    throw new Exception("Can't create FileLogger without a path to a file provided!");
                }
                default:
                {
                    throw new Exception("Unknown logger requested");
                }
            }
        }
        private static LoggerFactory? instance = null;

        public static LoggerFactory Instance
        {
            get
            {
                return instance ?? (instance = new LoggerFactory());
            }
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
    public class NullLogger : ILogger
    {
        public void Log(string message)
        {

        }
    }

    public class FileLogger : ILogger
    {
        string FilePath;
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        public void Log(string message)
        {
            StreamWriter sw = new StreamWriter(FilePath);
            sw.WriteLine(message);
            sw.Close();
        }
    }

}
