namespace AlfredPlugin
{
    public interface IAlfredPlugin
    {
        string Name { get; }

        bool Register();

        void Init();

        void Update();

    }
}
