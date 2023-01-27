using COOrm.Library.Infrastructure.Base;

namespace COOrm.Library.Interfaces.TableNameProviders;

public interface ITableNameProvider
{
    string GetTableName<TEntity>() where TEntity : BaseEntity;
    string GetTableName(BaseEntity entity);
    string GetTableName(Type type);
}
