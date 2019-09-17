using System;

namespace SuperBack
{
    class Program
    {
        static void Main(string[] args)
        {
            using(MainApp app = new MainApp().Init())
            {
                Console.WriteLine("Alfred says : Welcome master.");
                app.Run();
            }
        }

       
    }
}
