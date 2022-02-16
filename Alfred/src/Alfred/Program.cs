using System;

namespace Alfred
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