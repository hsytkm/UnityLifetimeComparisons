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

        //public bool IsDisposed { get; private set; }
        private bool _isDisposed;

        public Action? DisposeCallback { get; set; }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                DisposeCallback?.Invoke();
                _isDisposed = true;
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
