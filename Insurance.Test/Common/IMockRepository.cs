using Insurance.Core.Interfaces;
using System.Linq;

namespace Insurance.Test.Mocks
{
    public interface IMockRepository<out TContext>
    {
        void Add<T>(T e) where T : class, IEntity;

        void Remove<T>(T e) where T : class, IEntity;

        IQueryable<T> Query<T>() where T : class, IEntity;

        void Commit();

        TContext GetContext();
    }
}