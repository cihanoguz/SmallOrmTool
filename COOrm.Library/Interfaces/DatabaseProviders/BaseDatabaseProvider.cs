using COOrm.Infrastructure.SqlBuilders.Where;
using System.Data.Common;
using System.Linq.Expressions;
using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Infrastructure.Mapping;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.SqlProviders;
using COOrm.Library.Interfaces.TableNameProviders;
using System.Data.SqlClient;

namespace COOrm.Library.Interfaces.DatabaseProviders;
internal abstract class BaseDatabaseProvider : IDatabaseProvider
{
    public string ConnectionString { get; set; }
    public ITableNameProvider TableNameProvider { get; set; }
    public IColumnNameProvider ColumnNameProvider { get; set; }
    public ISelectBuilder SelectBuilder { get; set; }

    public IInsertSqlBuilder InsertSqlBuilder { get; set; }

    public abstract Task<int> Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;

    public abstract Task<IEnumerable<TEntity>> ReadList<TEntity>() where TEntity : BaseEntity;

    public abstract Task<IEnumerable<TEntity>> ReadList<TEntity>(Expression<Func<TEntity, bool>> expression = null) where TEntity : BaseEntity;


    public virtual string ToInsertSql<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        var mapping = Mapping.Instance.Get<TEntity>();

        if (mapping?.InsertSql is not null)
            return mapping.InsertSql;

        mapping ??= Mapping.Instance.SetMapping<TEntity>(TableNameProvider, ColumnNameProvider);

        mapping.InsertSql = InsertSqlBuilder.Build(entity, TableNameProvider, ColumnNameProvider);

        return mapping.InsertSql;
    }

    public virtual string ToSelectSql<TEntity>() where TEntity : BaseEntity
    {
        // SELECT * FROM [USER]

        var mapping = Mapping.Instance.Get<TEntity>();

        if (mapping?.SelectSql is not null)
            return mapping.SelectSql; // return cached


        mapping ??= Mapping.Instance.SetMapping<TEntity>(TableNameProvider, ColumnNameProvider);

        mapping.SelectSql = SelectBuilder.Build<TEntity>(TableNameProvider, ColumnNameProvider);

        return mapping.SelectSql;
    }

    protected virtual async Task<DbDataReader> GetReader(
            DbConnection conn,
            string sql,
            WherePart wherePart = null)
    {
        ArgumentNullException.ThrowIfNull(conn);

        if (conn.State != System.Data.ConnectionState.Open)
            await conn.OpenAsync(); // requires open connection

        var command = conn.CreateCommand();
        var logStr = "";
        if (wherePart is not null && wherePart.HasSql)
        {
            sql += $" WHERE {wherePart.Sql}";

            if (wherePart.Parameters.Any())
            {
                if (conn is SqlConnection)
                {
                    foreach (var parameter in wherePart.Parameters)
                    {
                        var sqlParam = new SqlParameter(parameter.Name, parameter.Value);
                        command.Parameters.Add(sqlParam);
                    }
                }
                else
                    throw new NotSupportedException($"{conn.GetType().Name} is not supported yet!");

                logStr += string.Join(",", wherePart.Parameters.Select(i => $"{i.Name}: {i.Value}"));
            }
        }

        logStr += $"SQL: \n {sql}";
        await Console.Out.WriteLineAsync(logStr);

        command.CommandText = sql;

        return command.ExecuteReader();
    }

    protected void SetObject<TEntity>(
            DbDataReader reader,
            out IList<TEntity> entities) where TEntity : BaseEntity
    {
        var objectMap = Mapping.Instance.Get<TEntity>();

        if (!objectMap.ColumnNamePropertyMap.Any())
        {
            Mapping.Instance.SetMapping<TEntity>(TableNameProvider, ColumnNameProvider);
        }

        entities = new List<TEntity>();

        while (reader.Read())
        {
            var ent = Activator.CreateInstance<TEntity>();
            entities.Add(ent);

            for (int i = 0; i < objectMap.ColumnNamePropertyMap.Count; i++)
            {
                var columnName = reader.GetName(i);
                var columnType = reader.GetFieldType(i);
                object data = reader.GetValue(i);
                var prop = objectMap.GetProperty(columnName);
                prop.SetValue(ent, Convert.ChangeType(data, columnType));
            }
        }
    }
}

