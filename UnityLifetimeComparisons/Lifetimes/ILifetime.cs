using System;
using System.Threading.Tasks;

namespace UnityLifetimeComparisons.Lifetimes
{
    interface ILifetime
    {
        Task<CheckupCertificate> CheckupAsync(int id);
    }
}
