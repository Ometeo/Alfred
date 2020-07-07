using System;

namespace SuperBack
{
    internal static class Program
    {
        private static void Main()
        {
            using MainApp app = new MainApp().Init();
            Console.WriteLine("Alfred says : Welcome master.");
            app.Run();
        }
    }
}