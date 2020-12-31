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
        static readonly Type[] _typeLifetimeTypes = new[]
        {
            typeof(TransientTypeLifetime),
            typeof(SingletonTypeLifetime),
            typeof(PerContainerTypeLifetime),
            typeof(PerContainerTransientTypeLifetime),
            typeof(HierarchicalTypeLifetime),
            typeof(PerResolveTypeLifetime),
            typeof(PerThreadTypeLifetime),
            typeof(ExternalTypeLifetime),
        };

        static readonly Type[] _instanceLifetimeTyps = new[]
        {
            typeof(PerContainerInstanceLifetime),
            typeof(SingletonInstanceLifetime),
            //typeof(ExternalInstanceLifetime),   // ◆Exception出るので調査するまで無効化
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine(GetAssemblyVersionString() + Environment.NewLine);

            Console.WriteLine("TypeLifetimes" + Environment.NewLine);
            await CheckupCommonAsync(_typeLifetimeTypes);
            CheckupIndividual(_typeLifetimeTypes);

            Console.WriteLine("InstanceLifetimes" + Environment.NewLine);
            await CheckupCommonAsync(_instanceLifetimeTyps);
            CheckupIndividual(_instanceLifetimeTyps);
        }

        static async Task CheckupCommonAsync(Type[] lifetimeTypes)
        {
            var lifetimes = lifetimeTypes.Select(static t => Activator.CreateInstance(t)).Cast<ILifetime>();
            var checkupTasks = lifetimes.Select(static async (x, i) => await x.CheckupAsync(i + 1));

            var checkupList = await Task.WhenAll(checkupTasks);
            FluentTextTable.Build.MarkdownTable<CheckupCertificate>().WriteLine(checkupList);
            Console.WriteLine(Environment.NewLine);

            foreach (var d in lifetimes.OfType<IDisposable>()) d.Dispose();
        }

        static void CheckupIndividual(Type[] lifetimeTypes)
        {
            var lifetimes = lifetimeTypes.Select(static t => Activator.CreateInstance(t)).Cast<ILifetime>();
            var messages = lifetimes.Select(static x => x.CheckupIndividual());

            foreach (var m in messages.OfType<string>())
            {
                Console.WriteLine("  - " + m + Environment.NewLine);
            }

            foreach (var d in lifetimes.OfType<IDisposable>()) d.Dispose();
        }

        static string GetAssemblyVersionString()
        {
            var type = typeof(UnityContainer);
            var assembly = Assembly.GetAssembly(type);
            return assembly?.GetName().ToString() ?? type.ToString();
        }
    }
}
