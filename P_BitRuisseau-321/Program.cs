namespace P_BitRuisseau_321
{
    internal static class Program
    {
        public static Dictionary<string, List<Song>> mediathequeSongs = new Dictionary<string, List<Song>>();
        public static List<Song> MySongs = new List<Song>();
        public static object MySongsLock = new object();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}