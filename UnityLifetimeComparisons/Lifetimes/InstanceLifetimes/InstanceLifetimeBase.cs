using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    abstract class InstanceLifetimeBase : LifetimeBase
    {
        public override string LifeTimeName
        {
            get
            {
                var lifetime = GetInstanceLifetimeManager();
                var name = lifetime.GetType().ToString()?.Split('.')[^1] ?? "Unknown";
                if (lifetime is IDisposable d) d.Dispose();
                return name;
            }
        }

        protected abstract IInstanceLifetimeManager GetInstanceLifetimeManager();
        protected override object GetLifetimeManager() => GetInstanceLifetimeManager();

        protected override void RegisterToContainer(IUnityContainer container)
        {
            var lifetimeManager = GetInstanceLifetimeManager();
            container.RegisterInstance<IService>(new Service(), lifetimeManager);
        }

        protected override bool CanReuseLifetimeManager() => false; //◆未実装

    }
}
