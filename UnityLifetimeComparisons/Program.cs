using System;
using System.Linq;
using System.Reflection;
using Unity;
using UnityLifetimeComparisons.Lifetimes;

namespace UnityLifetimeComparisons
{
    class Program
    {
        static readonly ILifetime[] _lifetimes = new ILifetime[]
        {
            new TransientLifetime(),
            new SingletonLifetime(),
            new ContainerControlledLifetime(),
            new ContainerControlledTransient(),
            new HierarchicalLifetime(),
            new PerResolveLifetime(),
            new PerThreadLifetime(),
            new ExternallyControlledLifetime(),
        };

        static void Main(string[] args)
        {
            Console.WriteLine(GetAssemblyVersionString() + Environment.NewLine);

            foreach (var lifetime in _lifetimes)
            {
                Console.WriteLine($"--- {lifetime.LifeTimeName} --- ");

                lifetime.DoTest();

                Console.WriteLine("\n\n");
            }

            foreach (var d in _lifetimes.OfType<IDisposable>())
            {
                d.Dispose();
            }
        }

        static string GetAssemblyVersionString()
        {
            var type = typeof(UnityContainer);
            var assembly = Assembly.GetAssembly(type);
            return assembly?.GetName().ToString() ?? type.ToString();
        }
    }
}
