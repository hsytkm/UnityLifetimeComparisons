using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    abstract class TypeLifetimeBase : LifetimeBase
    {
        public override string LifeTimeName
        {
            get
            {
                var lifetime = GetTypeLifetimeManager();
                var name = lifetime.GetType().ToString()?.Split('.')[^1] ?? "Unknown";
                if (lifetime is IDisposable d) d.Dispose();
                return name;
            }
        }

        protected abstract ITypeLifetimeManager GetTypeLifetimeManager();
        protected override object GetLifetimeManager() => GetTypeLifetimeManager();

        protected override void RegisterToContainer(IUnityContainer container)
        {
            var lifetimeManager = GetTypeLifetimeManager();
            container.RegisterType<IService, Service>(lifetimeManager);
        }

        protected override bool CanReuseLifetimeManager()
        {
            try
            {
                using (var container = CreateContainer())
                {
                    var lifetimeManager = GetTypeLifetimeManager();

                    // 同じ lifetimeManager を使用したいので、RegisterToContainer() は使用しない
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

    }
}
