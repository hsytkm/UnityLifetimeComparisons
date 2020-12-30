using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Unity;
using UnityLifetimeComparisons.Lifetimes;

namespace UnityLifetimeComparisons
{
    // https://github.com/nuitsjp/FluentTextTable
    // https://github.com/unitycontainer/abstractions/blob/2b99c9686bc01c3ef22ef294ec3afd558b4cb994/src/Extensions/Lifetime/TypeLifetime.cs
    // https://github.com/unitycontainer/abstractions/blob/2b99c9686bc01c3ef22ef294ec3afd558b4cb994/src/Extensions/Lifetime/InstanceLifetime.cs
    class Program
    {
        static readonly ILifetime[] _lifetimes = new ILifetime[]
        {
            new TransientTypeLifetime(),
            new SingletonTypeLifetime(),
            new PerContainerTypeLifetime(),
            new PerContainerTransientTypeLifetime(),
            new HierarchicalTypeLifetime(),
            new PerResolveTypeLifetime(),
            new PerThreadTypeLifetime(),
            new ExternalTypeLifetime(),
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
