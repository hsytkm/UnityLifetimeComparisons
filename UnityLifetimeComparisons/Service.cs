using System;

namespace UnityLifetimeComparisons
{
    interface IService : IEquatable<IService>
    {
        Guid Guid { get; }
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

        public bool Equals(IService? other)
        {
            if (other is null) return false;
            if (!ReferenceEquals(this, other)) return false;
            return this.Guid == other.Guid;
        }
    }
}
