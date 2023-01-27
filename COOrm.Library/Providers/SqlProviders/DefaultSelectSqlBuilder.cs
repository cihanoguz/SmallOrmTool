using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.SqlProviders;
using COOrm.Library.Interfaces.TableNameProviders;

namespace COOrm.Library.Providers.SqlProviders;
public class DefaultSelectSqlBuilder : ISelectBuilder
{
    string ISelectBuilder.Build<TEntity>(ITableNameProvider tableNameProvider,
                                         IColumnNameProvider columnNameProvider)
    {
        // SELECT * FROM USER
        var tableName = tableNameProvider.GetTableName<TEntity>();
        var columns = columnNameProvider.GetColumnName<TEntity>();
        string columnString = "*";

        if (columns is not null && columns.Any())
            columnString = string.Join(", ", columns.Select(i => $"[{i}]"));

        var generateSql = $"SELECT {columnString} FROM {tableName}";

        return generateSql;
    }
}
