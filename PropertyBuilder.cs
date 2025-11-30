
namespace SqlSugar.FluentMapping
{
    /// <summary>
    /// Property builder for configuring entity properties
    /// </summary>
    public class PropertyBuilder
    {
        private readonly EntityColumnInfo? _column;

        /// <summary>
        /// Dummy constructor
        /// </summary>
        public PropertyBuilder()
        {
            _column = null;
        }

        /// <summary>
        /// Property builder constructor
        /// </summary>
        /// <param name="column">Column from table</param>
        public PropertyBuilder(EntityColumnInfo column)
        {
            _column = column;
        }

        /// <summary>
        /// Table column name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyBuilder ColumnName(string name)
        {
            if (_column != null)
                _column.DbColumnName = name;

            return this;
        }

        /// <summary>
        /// The property is primary key
        /// </summary>
        /// <returns></returns>
        public PropertyBuilder IsPrimaryKey()
        {
            if (_column != null)
                _column.IsPrimarykey = true;

            return this;
        }

        /// <summary>
        /// The property is identity
        /// </summary>
        /// <returns></returns>
        public PropertyBuilder IsIdentity()
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
        public PropertyBuilder HasMaxLength(int length)
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
        public PropertyBuilder IsNullable()
        {
            if (_column != null)
                _column.IsNullable = true;

            return this;
        }

        /// <summary>
        /// The property is not nullable
        /// </summary>
        /// <returns></returns>
        public PropertyBuilder IsNotNull()
        {
            if (_column != null)
                _column.IsNullable = false;

            return this;
        }

        /// <summary>
        /// Ignore this property always (not mapped to any table column)
        /// </summary>
        /// <returns></returns>
        public PropertyBuilder IsIgnore()
        {
            if (_column != null)
                _column.IsIgnore = true;
            
            return this;
        }

        /// <summary>
        /// Ignore this property while inserting
        /// </summary>
        /// <returns></returns>
        public PropertyBuilder IsOnlyIgnoreInsert()
        {
            if (_column != null)
                _column.IsOnlyIgnoreInsert = true;

            return this;
        }

        /// <summary>
        /// Ignore this property while updating
        /// </summary>
        /// <returns></returns>
        public PropertyBuilder IsOnlyIgnoreUpdate()
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
        public PropertyBuilder ColumnDescription(string description)
        {
            if (_column != null)
                _column.ColumnDescription = description;

            return this;
        }
    }
}
