
namespace COOrm.Library.Infrastructure.Attributes;
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ColumnNameAttribute : Attribute
{
    public string ColumnName { get; private set; }

    public ColumnNameAttribute(string columnName)
    {
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
    }
}
