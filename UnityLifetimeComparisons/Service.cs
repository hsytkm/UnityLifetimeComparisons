using System;

namespace UnityLifetimeComparisons
{
    interface IService
    {
        Action? DisposeCallback { get; set; }
    }

    record Service : IService, IDisposable
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public bool IsDisposed { get; private set; }
        public Action? DisposeCallback { get; set; }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                DisposeCallback?.Invoke();
                IsDisposed = true;
            }
        }
    }
}
