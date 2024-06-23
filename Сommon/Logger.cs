
namespace Autofarm.Сommon
{
    public enum LogLevel
    {
        Debug = 1,
        Verbose = 2,
        Information = 3,
        Warning = 4,
        Error = 5,
        Critical = 6,
        None = int.MaxValue
    }

    public class Logger
    {
        private static Logger instance;

        private Logger() 
        {
        }

        public static Logger GetInstance() 
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }

        public void Log(LogLevel level, string nameGame, string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {level}: {nameGame} {message}");
        }
    }
}
