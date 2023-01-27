using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.TableNameProviders;

namespace COOrm.Library.Interfaces.SqlProviders;
/// <summary>
/// this interface for describing select operation behaviour on ORM
/// </summary>
public interface ISelectBuilder
{
    string Build<TEntity>(ITableNameProvider tableNameProvider,
                          IColumnNameProvider columnNameProvider) where TEntity : BaseEntity;
}
