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

    }
}
