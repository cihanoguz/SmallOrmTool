using COOrm.Infrastructure.SqlBuilders.Where;
using System.Data.Common;
using System.Linq.Expressions;
using System.Data.SqlClient;


namespace COOrm.Library.Interfaces.DatabaseProviders;
internal class SqlServerDatabaseProvider : BaseDatabaseProvider
{
    public override async Task<int> Insert<TEntity>(TEntity entity)
    {
        using var conn = new SqlConnection(ConnectionString);
        string sql = ToInsertSql(entity);

        if (conn.State != System.Data.ConnectionState.Open)
            await conn.OpenAsync();

        var command = conn.CreateCommand();
        command.CommandText = sql;

        return await command.ExecuteNonQueryAsync();
    }

    public override async Task<IEnumerable<TEntity>> ReadList<TEntity>()
    {
        using var conn = new SqlConnection(ConnectionString);

        var sql = ToSelectSql<TEntity>();

        DbDataReader reader = await GetReader(conn, sql);

        SetObject<TEntity>(reader, out var result);


        return result;
    }

    public override async Task<IEnumerable<TEntity>> ReadList<TEntity>(Expression<Func<TEntity, bool>> expression = null)
    {
        using var conn = new SqlConnection(ConnectionString);

        var entity = Activator.CreateInstance<TEntity>();
        string sql = ToSelectSql<TEntity>();

        DbDataReader reader = null;

        if (expression is not null)
        {
            var wherePart = expression.ToSql();

            reader = await GetReader(conn, sql, wherePart);
        }
        else
        {
            reader = await GetReader(conn, sql);
        }

        SetObject<TEntity>(reader, out var entities);

        return entities;
    }
}
