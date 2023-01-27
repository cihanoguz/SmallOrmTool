using System.Reflection;
using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Infrastructure.Configuration;
using COOrm.Library.Interfaces.ColumnNameProviders;

namespace COOrm.Library.Providers.ColumnNameProviders;
public class EntityTypeBuilderProvider : IColumnNameProvider
{
    public EntityTypeBuilderProvider()
    {
    }

    public Dictionary<PropertyInfo, string> CreateColumnMap(Type type)
    {
        return EntityTypeBuilderMapping.Instance.Map[type];
    }

    public IEnumerable<string> GetColumnName<TEntity>() where TEntity : BaseEntity
    {
        return GetColumnName(typeof(TEntity));
    }

    public IEnumerable<string> GetColumnName(Type type)
    {
        return EntityTypeBuilderMapping.Instance.Map[type].Values;
    }
}
