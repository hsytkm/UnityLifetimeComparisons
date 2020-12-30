using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

        static async Task Main(string[] args)
        {
            Console.WriteLine(GetAssemblyVersionString() + Environment.NewLine);

            //foreach (var lifetime in _lifetimes)
            //{
            //    Console.WriteLine($"--- {lifetime.LifeTimeName} --- ");
            //    lifetime.DoTest();
            //    Console.WriteLine("\n\n");
            //}

            var checkupTasks = _lifetimes.Select(async (x, i) => await x.CheckupAsync(i + 1));
            var checkupList = await Task.WhenAll(checkupTasks);
            FluentTextTable.Build.MarkdownTable<CheckupCertificate>().WriteLine(checkupList);

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
