using Insurance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Insurance.Test.Mocks
{
    internal class DbMockRepository<TDataContext> : IMockRepository<TDataContext>
        where TDataContext : DbContext
    {
        private readonly DbContextOptions _options;
        private TDataContext _db;

        public DbMockRepository()
        {
            var builder = new DbContextOptionsBuilder<TDataContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;

            _db = GetDbContext(_options);
        }

        public void Add<T>(T e)
            where T : class, IEntity
        {
            _db.Add(e);
        }

        public void Commit()
        {
            _db.SaveChanges();
            _db = GetDbContext(_options);
        }

        public IQueryable<T> Query<T>()
            where T : class, IEntity
        {
            return _db.Set<T>();
        }

        public void Remove<T>(T e)
            where T : class, IEntity
        {
            _db.Remove(e);
        }

        private static TDataContext GetDbContext(DbContextOptions options)
        {
            var db = (TDataContext)Activator.CreateInstance(typeof(TDataContext), options);
            return db;
        }

        public TDataContext GetContext()
        {
            return GetDbContext(_options);
        }
    }
}