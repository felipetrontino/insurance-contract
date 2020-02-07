using Insurance.Core.Interfaces;
using System;
using System.Linq;

namespace Insurance.Test.Mocks
{
    public class MockBuilder<TBuilder, TEntity> : IMockBuilder<TBuilder, TEntity>
        where TBuilder : class, IMockBuilder<TBuilder, TEntity>
        where TEntity : class, new()
    {
        public static TEntity Null => default;
        public static TEntity Empty => new TEntity();
        public string Key { get; set; }

        public TEntity Value { get; set; }

        public static TBuilder Create(string key = null)
        {
            var ret = Activator.CreateInstance<TBuilder>();
            ret.Key = key;
            ret.Value = CreateModel<TEntity>(ret.Key);

            return ret;
        }

        public TEntity Build()
        {
            return Value;
        }

        private static T CreateModel<T>(string key)
            where T : class, new()
        {
            var ret = new T();

            var idProp = typeof(T).GetProperties().FirstOrDefault(x => x.Name == nameof(IEntity.Id));

            if (idProp == null) return ret;

            var id = key != null ? Fake.GetId(key) : Guid.Empty;
            idProp.SetValue(ret, id);

            return ret;
        }
    }
}