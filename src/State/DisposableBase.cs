using System;
using System.Reactive.Disposables;

namespace State
{
    public class DisposableBase : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        protected CompositeDisposable Garbage = new CompositeDisposable();
    }
}