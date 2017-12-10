using System;

namespace TrafficSim
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static void DontRestart()
        {
            cont = false;
        }

        private static bool cont = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            while (cont)
            {
                using (var game = new Game1())
                    game.Run();
            }
        }
    }
#endif
}
