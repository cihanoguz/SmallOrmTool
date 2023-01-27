using System.Reflection;

namespace COOrm.Library.Infrastructure.Mapping;
internal class ObjectMap
{
    public ObjectMap(Type entity)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Type Entity { get; set; }

    public string SelectSql { get; set; }

    public string InsertSql { get; set; }

    public string TableName { get; set; }

    private IEnumerable<string> columnNames;
    public IEnumerable<string> ColumnNames
    {
        get => columnNames ??= ColumnNamePropertyMap?.Values;
        set => columnNames = value;
    }

    public Dictionary<PropertyInfo, string> ColumnNamePropertyMap { get; set; } = new();


    public void Add(PropertyInfo property, string columnName)
    { 
        var success = ColumnNamePropertyMap.TryAdd(property, columnName);

        if (!success)
        {
            throw new InvalidOperationException($"{property.Name} is already added");
        }
    }

    internal PropertyInfo GetProperty(string columnName)
    {
        return ColumnNamePropertyMap.Where(i => i.Value.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                                .Select(i => i.Key)
                                .FirstOrDefault();
    }

}
