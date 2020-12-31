using System;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.PerResolve is the same as PerResolveLifetimeManager.
    /// </summary>
    class PerResolveTypeLifetime : TypeLifetimeBase
    {
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerResolve;

        public override string? CheckupIndividual()
        {
            var sb = new StringBuilder();
            using var container = CreateContainer();

            container.RegisterType<ClassA>(TypeLifetime.PerResolve);
            container.RegisterType<ClassB>();
            container.RegisterType<ClassC>();

            var c1 = container.Resolve<ClassC>();
            if (ReferenceEquals(c1.ClsA, c1.ClsB.ClsA))
            {
                sb.Append("PerResolve : ClassC に共通の ClassA は 同じインスタンス");
            }

            var c2 = container.Resolve<ClassC>();
            if (!ReferenceEquals(c1.ClsA, c2.ClsA))
            {
                sb.AppendLine(" (でも異なる ClassC の各 ClassA は別インスタンス)");
            }

            return sb.ToString();
        }

        class ClassA { }

        class ClassB
        {
            public ClassA ClsA { get; }
            public ClassB(ClassA a) => ClsA = a;
        }

        class ClassC
        {
            public ClassA ClsA { get; }
            public ClassB ClsB { get; }
            public ClassC(ClassA a, ClassB b) => (ClsA, ClsB) = (a, b);
        }
    }
}
