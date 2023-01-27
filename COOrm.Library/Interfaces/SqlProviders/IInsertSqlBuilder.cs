using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.TableNameProviders;

namespace COOrm.Library.Interfaces.SqlProviders;

public interface IInsertSqlBuilder
{ 
    string Build<TEntity>(TEntity entity,
                          ITableNameProvider tableNameProvider,
                          IColumnNameProvider columnNameProvider) where TEntity : BaseEntity;
}
