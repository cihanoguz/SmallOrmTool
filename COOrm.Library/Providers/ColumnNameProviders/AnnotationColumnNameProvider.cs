using System.Reflection;
using COOrm.Library.Infrastructure.Attributes;
using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;

namespace COOrm.Library.Providers.ColumnNameProviders;
internal class AnnotationColumnNameProvider : IColumnNameProvider
{
    public Dictionary<PropertyInfo, string> CreateColumnMap(Type type)
    {
        var map = new Dictionary<PropertyInfo, string>();

        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<ColumnNameAttribute>();
            var columnName = attribute?.ColumnName ?? property.Name;

            map.Add(property, columnName);
        }

        return map;
    }

    public IEnumerable<string> GetColumnName<TEntity>() where TEntity : BaseEntity
    {
        return GetColumnName(typeof(TEntity));
    }

    public IEnumerable<string> GetColumnName(Type type)
    {
        var properties = type.GetProperties();
        var columns = new List<string>();

        foreach (var property in properties)
        {
            var att = property.GetCustomAttribute<ColumnNameAttribute>();
            var columnName = att?.ColumnName ?? property.Name;

            columns.Add(columnName);
        }

        return columns;
    }
}