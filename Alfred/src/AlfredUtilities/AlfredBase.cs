using System;

namespace AlfredUtilities
{
    public abstract class AlfredBase : IDisposable
    {
        #region Private Fields

        private bool disposed;

        #endregion Private Fields

        #region Private Destructors

        ~AlfredBase()
        {
            Dispose(false);
        }

        #endregion Private Destructors

        #region Public Methods

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DisposeManagedObjects();
                }

                DisposeUnmanagedObjects();
                disposed = true;
            }
        }

        protected abstract void DisposeManagedObjects();

        protected abstract void DisposeUnmanagedObjects();

        #endregion Protected Methods
    }
}
