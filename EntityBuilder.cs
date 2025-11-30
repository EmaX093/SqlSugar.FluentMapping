namespace SqlSugar.FluentMapping
{
    public interface IEntityMapping
    {
        void Configure(object builder);

    }
    public class EntityMappingWrapper<TEntity> : IEntityMapping
    {
        private readonly EntityBuilder<TEntity> _inner;

        public EntityMappingWrapper(EntityBuilder<TEntity> inner)
        {
            _inner = inner;
        }

        public void Configure(object builder)
        {
            // cast seguro, ya que sabemos que builder es EntityTypeBuilder<TEntity>
            //_inner.SetEntityColumnInfoInstance(((EntityTypeBuilder<TEntity>)builder)._entityColumnInfo!);
            //_inner.SetEntityInstance(((EntityTypeBuilder<TEntity>)builder)._entityInfo!);
            
            _inner.Configure((EntityTypeBuilder<TEntity>)builder);
        }
    }


    /// <summary>
    /// Abstract base class for entity builders
    /// </summary>
    /// <typeparam name="T">Target class</typeparam>
    public abstract class EntityBuilder<T>
    {
        public abstract void Configure(EntityTypeBuilder<T> builder);   
    }
}
