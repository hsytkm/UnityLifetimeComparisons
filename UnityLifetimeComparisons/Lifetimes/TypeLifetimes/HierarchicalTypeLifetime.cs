using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.Hierarchical is the same as HierarchicalLifetimeManager.
    /// </summary>
    class HierarchicalTypeLifetime : TypeLifetimeBase
    {
        // TypeLifetime.Hierarchical and TypeLifetime.Scoped are same.
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.Hierarchical;
        public override string? CheckupIndividual()
        {
            var parent = CreateContainer();
            var child = parent.CreateChildContainer();

            RegisterToContainer(parent);
            RegisterToContainer(child);

            var s1 = parent.Resolve<IService>();
            var s2 = child.Resolve<IService>();

            if (!ReferenceEquals(s1, s2))
            {
                return "Hierarchical : 親と子コンテナでインスタンスが異なる";
            }
            return null;
        }

    }
}
