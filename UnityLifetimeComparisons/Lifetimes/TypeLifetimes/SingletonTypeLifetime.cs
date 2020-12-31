using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.Singleton is the same as SingletonLifetimeManager.
    /// </summary>
    class SingletonTypeLifetime : TypeLifetimeBase
    {
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.Singleton;

        public override string? CheckupIndividual()
        {
            var preInstance = _container.Resolve<IService>();
            _container.RegisterType<IService, SpecialService>(TypeLifetime.Singleton);
            var newInstance = _container.Resolve<IService>();

            if (preInstance.GetType() != newInstance.GetType())
            {
                return "Singleton : 登録済み情報を上書きできる";
            }
            return null;
        }

        record SpecialService : Service;
    }
}
