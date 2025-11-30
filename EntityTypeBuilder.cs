using System.Linq.Expressions;

namespace SqlSugar.FluentMapping
{

    public class EntityTypeBuilder<T>
    {
        private readonly EntityInfo? _entityInfo;
        private readonly EntityColumnInfo? _entityColumnInfo;

        public EntityTypeBuilder(EntityInfo entity)
        {
            _entityInfo = entity;
        }

        public EntityTypeBuilder(EntityColumnInfo entityColumnInfo)
        {
            _entityColumnInfo = entityColumnInfo;
        }

        public EntityTypeBuilder<T> ToTable(string tableName)
        {
            if (_entityInfo != null)
            {
                _entityInfo.DbTableName = tableName;
            }

            return this;
        }

        public PropertyBuilder Property(Expression<Func<T, object>> expression)
        {
            // TODO: keep for a while, but remove later
            /*if (_entityInfo != null && _entityInfo.Columns != null)
            {
                var name = GetPropertyName(expression);
                var column = _entityInfo.Columns.First(x => x.PropertyName == name);
                return new PropertyBuilder(column);
            }*/

            // verify if is mapping at column level
            if (_entityColumnInfo != null)
            {
                if (_entityColumnInfo.PropertyName == GetPropertyName(expression))
                {
                    return new PropertyBuilder(_entityColumnInfo);
                }
            }

            // otherwise return a dummy builder
            return new PropertyBuilder();
        }

        private static string GetPropertyName(Expression<Func<T, object>> expr)
        {
            if (expr.Body is MemberExpression m) return m.Member.Name;
            if (expr.Body is UnaryExpression u && u.Operand is MemberExpression um) return um.Member.Name;
            throw new Exception("Invalid expression");
        }
    }



}
