using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;

namespace UnityLifetimeComparisons.Lifetimes
{
    abstract class LifetimeBase : ILifetime, IDisposable
    {
        private bool _isDisposed = false;
        protected readonly IUnityContainer _container;

        public abstract string LifeTimeName { get; }

        public LifetimeBase() => _container = CreateAndRegisterContainer();

        protected static IUnityContainer CreateContainer() => new UnityContainer();

        private IUnityContainer CreateAndRegisterContainer()
        {
            var container = CreateContainer();
            RegisterToContainer(container);
            return container;
        }

        protected abstract void RegisterToContainer(IUnityContainer container);

        #region Checkups
        protected abstract object GetLifetimeManager();

        private bool CanDisposableLifetime()
        {
            var lifetile = GetLifetimeManager();
            if (lifetile is IDisposable d)
            {
                d.Dispose();
                return true;
            }
            return false;
        }

        protected static bool IsResolveEquals(IUnityContainer container1, IUnityContainer container2)
        {
            var s1 = container1.Resolve<IService>();
            var s2 = container2.Resolve<IService>();
            return s1.Equals(s2);
        }

        private bool IsEqualInstanceFromSameContainer() => IsResolveEquals(_container, _container);

        private bool IsEqualInstanceFromParentChildContainer()
        {
            var parent = _container;
            var child = _container.CreateChildContainer();
            return IsResolveEquals(parent, child);
        }

        private async Task<bool> IsEqualInstanceFromSameContainerOnOtherThreadAsync()
        {
            var s1 = _container.Resolve<IService>();
            var s2 = await Task.Run(() => _container.Resolve<IService>());
            return s1.Equals(s2);
        }

        private bool IsDisposedRegisteredInstance()
        {
            var finalized = false;
            using (var container = CreateAndRegisterContainer())
            {
                var service = container.Resolve<IService>();
                service.DisposeCallback = () => { finalized = true; };
            }
            return finalized;
        }

        protected abstract bool CanReuseLifetimeManager();
        #endregion

        public async Task<CheckupCertificate> CheckupAsync(int id) => new CheckupCertificate()
        {
            Id = id,
            LifetimeName = LifeTimeName,
            CanDisposableLifetime = CanDisposableLifetime(),
            IsEqualInstanceFromSameContainer = IsEqualInstanceFromSameContainer(),
            IsEqualInstanceFromParentChildContainer = IsEqualInstanceFromParentChildContainer(),
            IsEqualInstanceFromSameContainerOnOtherThread = await IsEqualInstanceFromSameContainerOnOtherThreadAsync(),
            IsDisposedRegisteredInstance = IsDisposedRegisteredInstance(),
            CanReuseLifetimeManager = CanReuseLifetimeManager(),
        };

        public abstract string? CheckupIndividual();

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _container.Dispose();
                _isDisposed = true;
            }
        }
    }
}
