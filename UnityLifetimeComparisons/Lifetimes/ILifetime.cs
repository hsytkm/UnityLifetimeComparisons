using System;
using System.Threading.Tasks;

namespace UnityLifetimeComparisons.Lifetimes
{
    interface ILifetime
    {
        string LifeTimeName { get; }
        Task<CheckupCertificate> CheckupAsync(int id);
    }
}
