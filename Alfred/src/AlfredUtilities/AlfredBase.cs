using System;

namespace AlfredUtilities
{
    public abstract class AlfredBase : IDisposable
    {
        private bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        ~AlfredBase()
        {
            Dispose(false);
        }
    }
}