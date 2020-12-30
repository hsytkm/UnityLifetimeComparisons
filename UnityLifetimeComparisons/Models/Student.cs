using System;

namespace UnityLifetimeComparisons.Models
{
    interface IPerson
    {
        string Name { get; }
    }

    record Student(string Name, int Id) : IPerson { }
}
