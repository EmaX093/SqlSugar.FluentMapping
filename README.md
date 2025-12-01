# SqlSugar.FluentMapping

This extension add Fluent Mapping feature to SqlSugar ORM.

SqlSugar ORM
https://github.com/DotNetNext/SqlSugar

SqlSugar Documentation (CN)
https://www.donet5.com/home/doc

SqlSugar Documentation (EN / Partial)
https://github.com/DotNetNext/SqlSugar/wiki

## Overview
### Supported SDKs
Supported SDKs: .NET Core 6 onwards is supported.

### What is SqlSugar.FluentMapping?
SqlSugar.FluentMapping allows you to configure entity mappings using a fluent API, similar to other ORM Fluent API for model configuration like EF, NHibernate, etc.

The main goal is to map entities without using attributes, keeping entity classes clean and separated from mapping concerns.

It follows the SqlSugar conventions and it's easy to use. Please refer to the examples below to get started or check SQLSugar documentation for more details about SqlSugar features.

All contributions and feedback are welcome. Feel free to open issues or submit pull requests.

❤️ With love from Buenos Aires, Argentina ❤️
 
## Installation
You can install the SqlSugar.FluentMapping package via NuGet Package Manager Console or Package Manager UI.
```
Install-Package SqlSugar.FluentMapping
```

## Usage

### Example model class
```csharp
namespace My.Domain.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public long CUIT { get; set; }
        public string BusinessName { get; set; }
        public bool IsActive { get; set; }
        public CustomerSubscription Subscription { get; set; }
        public List<CustomerPrepaidPackHistory> PrepaidPacks { get; set; }
    }
}
```

### Step 1 - Create map classes to map entities with Fluent Mapping
```csharp
public class CustomerMap : EntityBuilder<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("clientes");   // SE EJECUTA A TIEMPO

        builder.Property(x => x.Id)
            .ColumnName("id")
            .IsPrimaryKey()
            .IsIdentity();

        builder.Property(x => x.BusinessName)
                .ColumnName("razon_social");

        builder.Property(x => x.IsActive)
            .ColumnName("habilitado");

        builder.Property(x => x.Subscription)
            .IsIgnore();

        builder.Property(x => x.PrepaidPacks)
            .IsIgnore();

    }
}   
```

### Step 2 - Add map classes & register Fluent Mappings
```csharp
using SqlSugar;
using SqlSugar.FluentMapping;

// Create SqlSugarClient/Scoped or use your existing one
var db = new SqlSugarClient(new ConnectionConfig()
{
    ConnectionString = "your_connection_string",
    DbType = DbType.SqlServer,
    IsAutoCloseConnection = true,
});


// Register Fluent Mappings
db.ApplyFluentMapping(new CustomerMap(), new UserMap(), new EmailMap());
```


## License
This extension is under Apache License 2.0. Refer to the LICENSE file for more information.
