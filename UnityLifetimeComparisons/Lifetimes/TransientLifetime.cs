﻿using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class TransientLifetime : LifetimeBase
    {

        public TransientLifetime() : base(TypeLifetime.Transient, typeof(TransientLifetimeManager))
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
