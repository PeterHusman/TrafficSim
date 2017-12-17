using System;

namespace TrafficSim
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static void Restart()
        {
            cont = true;
        }

        private static bool cont = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            do
            {
                cont = false;
                using (var game = new Game1())
                    game.Run();
            } while (cont);
        }
    }
#endif
}
