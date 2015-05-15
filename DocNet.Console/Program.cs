using log4net;

namespace DocNet.Console
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Log.Info("Welcome to DocNet!");
            System.Console.ReadLine();
        }
    }
}
