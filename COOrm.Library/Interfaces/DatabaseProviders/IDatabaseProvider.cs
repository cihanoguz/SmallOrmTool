using System.Linq.Expressions;
using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.SqlProviders;
using COOrm.Library.Interfaces.TableNameProviders;

namespace COOrm.Library.Interfaces.DatabaseProviders;
public interface IDatabaseProvider
{
    string ConnectionString { get; set; }

    ITableNameProvider TableNameProvider { get; set; }
    IColumnNameProvider ColumnNameProvider { get; set; }

    ISelectBuilder SelectBuilder { get; set; }

    IInsertSqlBuilder InsertSqlBuilder { get; set; }

    string ToSelectSql<TEntity>() where TEntity : BaseEntity;

    Task<int> Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;

    Task<IEnumerable<TEntity>> ReadList<TEntity>() where TEntity : BaseEntity;

    Task<IEnumerable<TEntity>> ReadList<TEntity>(Expression<Func<TEntity, bool>> expression = null) where TEntity : BaseEntity;
}
