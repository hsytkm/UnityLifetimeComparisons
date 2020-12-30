using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    interface ILifetime
    {
        string LifeTimeName { get; }
        void DoTest();
        //bool IsDisposedRegisteredSingleton();
    }

    abstract class LifetimeBase<T> : ILifetime, IDisposable
        where T : ITypeLifetimeManager
    {
        private bool _isDisposed = false;

        protected readonly IUnityContainer _container;
        //protected readonly ITypeLifetimeManager _lifetimeManager;

        public string LifeTimeName => typeof(T).ToString().Split('.')[^1];

        public LifetimeBase(ITypeLifetimeManager lifetimeManager)
        {
            //_lifetimeManager = lifetimeManager;
            _container = CreateContainer();
        }

        private static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            var lifetimeManager = Activator.CreateInstance<T>();
            container.RegisterType<IService, Service>(lifetimeManager);
            return container;
        }

        protected static bool IsResolveEquals(IUnityContainer container1, IUnityContainer container2)
        {
            var s1 = container1.Resolve<IService>();
            var s2 = container2.Resolve<IService>();
            return ReferenceEquals(s1, s2);
        }
        protected static bool IsResolveEquals(IUnityContainer container) => IsResolveEquals(container, container);
        protected bool IsResolveEquals() => IsResolveEquals(_container);

        private static bool IsDisposed()
        {
            var finalized = false;
            using (var container = CreateContainer())
            {
                var service = container.Resolve<IService>();
                service.DisposeCallback = () => { finalized = true; };
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return finalized;
        }

        private static bool CanReuseLifetimeManager()
        {
            try
            {
                using (var container = new UnityContainer())
                {
                    var lifetimeManager = Activator.CreateInstance<T>();
                    container.RegisterType<IService, Service>(lifetimeManager);
                    container.RegisterType<IService, Service>(lifetimeManager);
                }
                return true;
            }
            catch (InvalidOperationException)
            {
                // The lifetime manager is already registered.
                // WithLifetime managers cannot be reused, please create a new one.
                return false;
            }
        }

        private bool IsEqualInstanceFromSameContainer() => IsResolveEquals(_container, _container);

        private bool IsEqualInstanceFromParentChildContainer()
        {
            var parent = _container;
            var child = _container.CreateChildContainer();
            return IsResolveEquals(parent, child);
        }

        public virtual void DoTest()
        {
            Console.WriteLine($"IsEqualInstanceFromSameContainer : {IsEqualInstanceFromSameContainer()}");
            Console.WriteLine($"IsEqualInstanceFromParentChildContainer : {IsEqualInstanceFromParentChildContainer()}");
            Console.WriteLine($"IsDisposed : {IsDisposed()}");
            Console.WriteLine($"CanReuseLifetimeManager : {CanReuseLifetimeManager()}");
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
