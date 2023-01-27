using System.Numerics;
using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.SqlProviders;
using COOrm.Library.Interfaces.TableNameProviders;

namespace COOrm.Library.Providers.SqlProviders;
internal class DefaultInsertSqlBuilder : IInsertSqlBuilder
{
    public string Build<TEntity>(TEntity entity, ITableNameProvider tableNameProvider, IColumnNameProvider columnNameProvider) where TEntity : BaseEntity
    {
        // INSERT INTO [TableName] ([COLUMNS]) VALUES ([VALUES])

        // INSERT INTO [User] (Id, UserName) VALUES (1, 'Cihan')
        var tableName = tableNameProvider.GetTableName<TEntity>();
        var columns = columnNameProvider.GetColumnName<TEntity>();
        string columnString = string.Join(',', columns);


        List<string> values = new List<string>();

        foreach (var property in typeof(TEntity).GetProperties())
        {
            object val = null;
            // this is the why i used .Net7 
            if (property.PropertyType.IsAssignableFrom(typeof(ISignedNumber<>)))
                val = property.GetValue(entity, null);
            else
                val = $"'{property.GetValue(entity, null)}'";

            values.Add(val.ToString());
        }

        string valuesString = string.Join(',', values);

        var generatedSql = $"INSERT INTO {tableName} ({columnString}) VALUES({valuesString})";

        return generatedSql;
    }
}
