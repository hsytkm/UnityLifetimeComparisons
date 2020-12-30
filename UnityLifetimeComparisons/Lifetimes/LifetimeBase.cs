using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    interface ILifetime
    {
        void DoTest();
        //bool IsDisposedRegisteredSingleton();
    }

    abstract class LifetimeBase : ILifetime, IDisposable
    {
        private bool _isDisposed = false;

        protected readonly IUnityContainer _container;
        protected readonly ITypeLifetimeManager _lifetimeManager;
        protected readonly Type _lifetimeType;

        public string LifeTimeName => _lifetimeManager.GetType().ToString().Split('.')[^1];

        public LifetimeBase(ITypeLifetimeManager lifetimeManager, Type lifetimeType)
        {
            _lifetimeManager = lifetimeManager;
            _lifetimeType = lifetimeType;
            _container = CreateContainer();
        }

        private IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            var lifetimeManager = (ITypeLifetimeManager)Activator.CreateInstance(_lifetimeType);
            container.RegisterType<IService, Service>(lifetimeManager);
            return container;
        }

        public abstract void DoTest();

        protected static bool IsResolveEquals(IUnityContainer container1, IUnityContainer container2)
        {
            var s1 = container1.Resolve<IService>();
            var s2 = container2.Resolve<IService>();
            return ReferenceEquals(s1, s2);
        }
        protected static bool IsResolveEquals(IUnityContainer container) => IsResolveEquals(container, container);
        protected bool IsResolveEquals() => IsResolveEquals(_container);

        protected static IService ResolveService(IUnityContainer container) => container.Resolve<IService>();
        protected IService ResolveService() => ResolveService(_container);

        public bool IsDisposed()
        {
            var finalized = false;

            using var container = CreateContainer();
            var obj = ResolveService(container);
            obj.DisposeCallback = () => { finalized = true; };

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return finalized;
        }

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
