namespace Insurance.Test.Mocks
{
    public interface IMockBuilder<TBuilder, TEntity>
         where TBuilder : IMockBuilder<TBuilder, TEntity>
         where TEntity : class, new()
    {
        string Key { get; set; }

        TEntity Value { get; set; }

        TEntity Build();
    }
}