using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Infrastructure.Configuration;
using COOrm.Library.Interfaces.DatabaseProviders;

namespace COOrm.Library.DbContext;
public class COContextOptions
{
    private List<Type> entities = new();
    public IDatabaseProvider DatabaseProvider { get; set; }

    public void AddEntity(Type type)
    {
        entities.Add(type);
    }
}

public class COContextOptionsBuilder : IDisposable
{
    private List<Type> entities = new();
    private IDatabaseProvider databaseProvider;

    public COContextOptionsBuilder AddEntity<TEntity>() where TEntity : BaseEntity
    {
        entities.Add(typeof(TEntity));
        return this;
    }

    public void AddEntityConfiguration<TEntity>(IEntityTypeConfiguration<TEntity> configuration)
       where TEntity : BaseEntity
    {
        entities.Add(typeof(TEntity));
        configuration.Build(new EntityTypeBuilder<TEntity>());
        //ConfigurationAdded = true;
    }

    public COContextOptionsBuilder SetDatabaseProvider(IDatabaseProvider provider)
    {
        databaseProvider = provider; 
        return this;
    }

    public COContextOptions Build()
    { 
        var result = new COContextOptions();

        foreach (var entity in entities)
        {
            result.AddEntity(entity.GetType());
        }

        result.DatabaseProvider = databaseProvider;

        return result;
    }

    public void Dispose()
    {
        entities.Clear();
        entities = null;

        GC.SuppressFinalize(this);
    }
}
