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

        //public override void DoTest()
        //{
        //    base.DoTest();

        //    // シングルトンは登録を上書きできる
        //    var preInstance = _container.Resolve<IService>();
        //    _container.RegisterInstance<IService>(new Service(), InstanceLifetime.Singleton);
        //    var newInstance = _container.Resolve<IService>();

        //    if (!preInstance.Equals(newInstance))
        //    {
        //        Console.WriteLine("Override Registered Type");
        //    }

        //}

    }
}
