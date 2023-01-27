using COOrm.Library.DbContext;
using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.SqlProviders;
using COOrm.Library.Interfaces.TableNameProviders;
using COOrm.Library.Providers.ColumnNameProviders;
using COOrm.Library.Providers.SqlProviders;
using COOrm.Library.Providers.TableNameProviders;

namespace COOrm.Library.Interfaces.DatabaseProviders;
public static class COContextExtensions
{
    public static void UseSqlServer(this COContextOptionsBuilder builder,
                                    string connectionString)
    {
        var provider = new SqlServerDatabaseProvider()
        {
            ConnectionString = connectionString,
            ColumnNameProvider = new EntityTypeBuilderProvider(),
            TableNameProvider = new AnnotationTableNameProvider(),
            SelectBuilder = new DefaultSelectSqlBuilder(),
            InsertSqlBuilder = new DefaultInsertSqlBuilder()
        };

        builder.SetDatabaseProvider(provider);
    }
}