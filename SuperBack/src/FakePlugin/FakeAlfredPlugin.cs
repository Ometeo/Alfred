using AlfredPlugin;

namespace FakePlugin
{
    class FakeAlfredPlugin : IAlfredPlugin
    {
        public string Name => "Fake plugin";

        public void Init()
        {
            System.Console.WriteLine("do nothin");
        }

        public bool Register()
        {
            return false;
        }

        public void Update()
        {
            System.Console.WriteLine("Update nothing");
        }
    }
}
