using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// InstanceLifetime.Singleton is the same as SingletonLifetimeManager.
    /// </summary>
    class SingletonInstanceLifetime : InstanceLifetimeBase
    {
        protected override IInstanceLifetimeManager GetInstanceLifetimeManager() => InstanceLifetime.Singleton;

        public override string? CheckupIndividual()
        {
            // シングルトンは登録済み情報を上書きできる
            var preInstance = _container.Resolve<IService>();

            _container.RegisterInstance<IService>(new Service(), InstanceLifetime.Singleton);
            var newInstance = _container.Resolve<IService>();

            if (preInstance.Equals(newInstance)) throw new Exception("謎ぃ");

            return "Singleton can override registered type.";
        }

    }
}
