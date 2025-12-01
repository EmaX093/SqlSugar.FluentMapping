namespace SqlSugar.FluentMapping
{
    public interface IEntityMapping
    {
        void Configure(object builder);

    }
    public class EntityMappingWrapper<TEntity> : IEntityMapping
    {
        private readonly EntityMapBuilder<TEntity> _inner;

        public EntityMappingWrapper(EntityMapBuilder<TEntity> inner)
        {
            _inner = inner;
        }

        public void Configure(object builder)
        {
            // cast seguro, ya que sabemos que builder es EntityTypeBuilder<TEntity>
            //_inner.SetEntityColumnInfoInstance(((EntityTypeBuilder<TEntity>)builder)._entityColumnInfo!);
            //_inner.SetEntityInstance(((EntityTypeBuilder<TEntity>)builder)._entityInfo!);
            
            _inner.Configure((EntityMapTypeBuilder<TEntity>)builder);
        }
    }


    /// <summary>
    /// Abstract base class for entity builders
    /// </summary>
    /// <typeparam name="T">Target class</typeparam>
    public abstract class EntityMapBuilder<T>
    {
        public abstract void Configure(EntityMapTypeBuilder<T> builder);   
    }
}
