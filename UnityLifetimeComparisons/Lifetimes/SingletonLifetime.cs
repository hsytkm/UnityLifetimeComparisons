using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class SingletonLifetime : LifetimeBase
    {
        public SingletonLifetime() : base(TypeLifetime.Singleton, typeof(SingletonLifetimeManager))
        {
        }

        public override void DoTest()
        {
            var parent = _container;
            var child = _container.CreateChildContainer();
            Console.WriteLine($"IsResolveEquals : {IsResolveEquals(parent, child)}");

            Console.WriteLine($"IsDisposed : {IsDisposed()}");


        }

    }
}
