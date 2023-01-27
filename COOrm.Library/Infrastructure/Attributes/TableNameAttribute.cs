
namespace COOrm.Library.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TableNameAttribute : Attribute
{
    public string TableName { get; private set; }

    public string SchemaName { get; private set; }

    public TableNameAttribute(string tableName)
    {
        TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
    }

    public TableNameAttribute(string tableName, string schemaName)
        : this(tableName)
    {
        SchemaName = schemaName ?? throw new ArgumentNullException(nameof(schemaName));
    }
}
