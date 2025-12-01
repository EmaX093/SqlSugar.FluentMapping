
namespace SqlSugar.FluentMapping
{
    /// <summary>
    /// Property builder for configuring entity properties
    /// </summary>
    public class MapPropertyBuilder
    {
        private readonly EntityColumnInfo? _column;

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

        /// <summary>
        /// Table column name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MapPropertyBuilder ColumnName(string name)
        {
            if (_column != null)
                _column.DbColumnName = name;

            return this;
        }

        /// <summary>
        /// The property is primary key
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder IsPrimaryKey()
        {
            if (_column != null)
                _column.IsPrimarykey = true;

            return this;
        }

        /// <summary>
        /// The property is identity
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder IsIdentity()
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
        public MapPropertyBuilder HasMaxLength(int length)
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
        public MapPropertyBuilder IsNullable()
        {
            if (_column != null)
                _column.IsNullable = true;

            return this;
        }

        /// <summary>
        /// The property is not nullable
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder IsNotNull()
        {
            if (_column != null)
                _column.IsNullable = false;

            return this;
        }

        /// <summary>
        /// Ignore this property always (not mapped to any table column)
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder Ignore()
        {
            if (_column != null)
                _column.IsIgnore = true;
            
            return this;
        }

        /// <summary>
        /// Ignore this property while inserting
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder IsOnlyIgnoredAtInsert()
        {
            if (_column != null)
                _column.IsOnlyIgnoreInsert = true;

            return this;
        }

        /// <summary>
        /// Ignore this property while updating
        /// </summary>
        /// <returns></returns>
        public MapPropertyBuilder IsOnlyIgnoredAtUpdate()
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
        public MapPropertyBuilder ColumnDescription(string description)
        {
            if (_column != null)
                _column.ColumnDescription = description;

            return this;
        }
    }
}
