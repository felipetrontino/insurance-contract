using System;
using System.Threading.Tasks;

namespace Insurance.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}