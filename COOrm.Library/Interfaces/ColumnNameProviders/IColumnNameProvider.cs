using System.Reflection;
using COOrm.Library.Infrastructure.Base;

namespace COOrm.Library.Interfaces.ColumnNameProviders;
public interface IColumnNameProvider
{
    IEnumerable<string> GetColumnName<TEntity>() where TEntity : BaseEntity;
    IEnumerable<string> GetColumnName(Type type);

    Dictionary<PropertyInfo, string> CreateColumnMap(Type type);
}
