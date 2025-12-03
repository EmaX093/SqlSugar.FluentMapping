
using System.Linq.Expressions;

namespace SqlSugar.FluentMapping
{
    /// <summary>
    /// Property builder for configuring entity properties
    /// </summary>
    public class MapPropertyBuilder<T>
    {
        private readonly EntityColumnInfo? _column;
        private readonly EntityInfo? _entity;

        /// <summary>
        /// Dummy constructor
        /// </summary>
        public MapPropertyBuilder()
        {
            _column = null;
        }

        /// <summary>
        /// Property builder constructor
        /// </summary>
        /// <param name="column">Column from table</param>
        public MapPropertyBuilder(EntityColumnInfo column)
        {
            _column = column;
        }

        public MapPropertyBuilder(EntityInfo entity)
        {
            _entity = entity;
        }

        /// <summary>
        /// Table column name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MapPropertyBuilder<T> ColumnName(string name)
        {
            if (_column != null)
                _column.DbColumnName = name;

            return this;
        }

        /// <summary>
        /// The property is primary key
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder<T> IsPrimaryKey()
        {
            if (_column != null)
                _column.IsPrimarykey = true;

            return this;
        }

        /// <summary>
        /// Is a self-increment column. If Oracle 12+ is enabled, see documentation: Oracle. For Oracle 11 or later, set the OracleSequenceName and use the value as the automatic increment.
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder<T> IsIdentity()
        {
            if (_column != null)
                _column.IsIdentity = true;

            return this;
        }

        /// <summary>
        /// Property max length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public MapPropertyBuilder<T> HasMaxLength(int length)
        {
            if (_column != null)
                _column.Length = length;

            return this;
        }

        /// <summary>
        /// Property is nullable
        /// </summary>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public MapPropertyBuilder<T> IsNullable()
        {
            if (_column != null)
                _column.IsNullable = true;

            return this;
        }

        /// <summary>
        /// The property is not nullable
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder<T> IsNotNull()
        {
            if (_column != null)
                _column.IsNullable = false;

            return this;
        }

        /// <summary>
        /// Ignore this property always (not mapped to any table column)
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder<T> Ignore()
        {
            if (_column != null)
                _column.IsIgnore = true;
            
            return this;
        }

        /// <summary>
        /// This column is not processed during insert operation. It is valid for the database default value
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder<T> IsOnlyIgnoredAtInsert()
        {
            if (_column != null)
                _column.IsOnlyIgnoreInsert = true;

            return this;
        }

        /// <summary>
        /// Ignore this property while updating
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder<T> IsOnlyIgnoredAtUpdate()
        {
            if (_column != null)
                _column.IsOnlyIgnoreUpdate = true;

            return this;
        }

        /// <summary>
        /// Set the column description in table database
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public MapPropertyBuilder<T> ColumnDescription(string description)
        {
            if (_column != null)
                _column.ColumnDescription = description;

            return this;
        }

        public void OneToOne(Expression<Func<T, object>> expression, string firstName, string lastName = null)
        {
            if (_column != null)
            {
                var columnable = new EntityColumnable<T>(); // _column);
                columnable.entityColumnInfo = _column;
                columnable.OneToOne(expression, firstName, lastName);
            }
        }

        public void OneToMany(Expression<Func<T, object>> expression, string firstName, string lastName = null)
        {
            if (_column != null)
            {
                var columnable = new EntityColumnable<T>(); // _column);
                columnable.entityColumnInfo = _column;
                columnable.OneToMany(expression, firstName, lastName);
            }
        }

        public void ManyToMany(Expression<Func<T, object>> expression, Type mappingType, string mappingTypeAid, string mappingTypeBid)
        {
            if (_column != null)
            {
                var columnable = new EntityColumnable<T>(); // _column);
                columnable.entityColumnInfo = _column;
                columnable.ManyToMany(expression, mappingType, mappingTypeAid, mappingTypeBid);
            }
        }
    }
}
