using System;

namespace Solar_System_Warfare
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SolarSystemWarfareGame())
                game.Run();
        }
    }
}
