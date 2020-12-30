using System;
using System.Reflection;
using Unity;
using UnityLifetimeComparisons.Lifetimes;

namespace UnityLifetimeComparisons
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetAssemblyVersionString() + Environment.NewLine);

            var lifetimes = new LifetimeBase[]
            {
                new TransientLifetime(),
                new SingletonLifetime(),
            };

            foreach (var lifetime in lifetimes)
            {
                Console.WriteLine($"--- {lifetime.LifeTimeName} --- ");

                lifetime.DoTest();

                Console.WriteLine("\n\n");
            }

            foreach (var d in lifetimes)
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
