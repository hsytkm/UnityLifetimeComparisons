﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    interface ILifetime
    {
        string TypeLifeTimeName { get; }
        void DoTest();
        Task<CheckupCertificate> CheckupAsync(int id);
    }

    abstract class LifetimeBase : ILifetime, IDisposable
    {
        private bool _isDisposed = false;

        protected readonly IUnityContainer _container;

        public abstract string TypeLifeTimeName { get; }

        public LifetimeBase()
        {
            _container = CreateContainer();
        }

        protected abstract ITypeLifetimeManager GetTypeLifetimeManager();

        private IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            var lifetimeManager = GetTypeLifetimeManager();
            container.RegisterType<IService, Service>(lifetimeManager);
            return container;
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

        private bool CanReuseLifetimeManager()
        {
            try
            {
                using (var container = new UnityContainer())
                {
                    var lifetimeManager = GetTypeLifetimeManager();
                    container.RegisterType<IService, Service>(lifetimeManager);
                    container.RegisterType<IPerson, Person>(lifetimeManager);
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

        public virtual void DoTest()
        {
            //Console.WriteLine($"IsEqualInstanceFromSameContainer : {IsEqualInstanceFromSameContainer()}");
            //Console.WriteLine($"IsEqualInstanceFromParentChildContainer : {IsEqualInstanceFromParentChildContainer()}");
            //Console.WriteLine($"IsDisposed : {IsDisposedRegisteredInstance()}");
            //Console.WriteLine($"CanReuseLifetimeManager : {CanReuseLifetimeManager()}");
        }

        public async Task<CheckupCertificate> CheckupAsync(int id) => new CheckupCertificate()
        {
            Id = id,
            TypeLifetimeName = TypeLifeTimeName,
            IsEqualInstanceFromSameContainer = IsEqualInstanceFromSameContainer(),
            IsEqualInstanceFromParentChildContainer = IsEqualInstanceFromParentChildContainer(),
            IsEqualInstanceFromSameContainerOnOtherThread = await IsEqualInstanceFromSameContainerOnOtherThreadAsync(),
            IsDisposedRegisteredInstance = IsDisposedRegisteredInstance(),
            CanReuseLifetimeManager = CanReuseLifetimeManager(),
        };

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
