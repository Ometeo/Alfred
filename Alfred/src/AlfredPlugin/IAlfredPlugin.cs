using AlfredUtilities.Messages;

namespace AlfredPlugin
{
    public interface IAlfredPlugin
    {
        #region Public Properties

        string Name { get; }

        #endregion Public Properties

        #region Public Methods

        void Init(IMessageDispatcher messageDispatcher);

        bool Register();

        void Update();

        #endregion Public Methods
    }
}
