using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using COOrm.Library.Infrastructure.Base;

namespace COOrm.Library.DbContext;
public class COContext
{
    private readonly COContextOptionsBuilder coContextOptionsBuilder;

    private COContextOptions _opt;
    private COContextOptions options => _opt ??= coContextOptionsBuilder.Build();

    public COContext(COContextOptionsBuilder coContextOptionsBuilder)
    {
        this.coContextOptionsBuilder = coContextOptionsBuilder;
    }

    public COContext(Action<COContextOptionsBuilder> coContextOptionsBuilderAction)
    {
        coContextOptionsBuilder = new();
        coContextOptionsBuilderAction(coContextOptionsBuilder);
    }

    public Task<int> Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        return options.DatabaseProvider.Insert(entity);
    }

    public Task<IEnumerable<TEntity>> ReadList<TEntity>() where TEntity : BaseEntity
    {
        var entities = options.DatabaseProvider.ReadList<TEntity>();

        return entities;
    }

    public async Task<IEnumerable<TEntity>> ReadList<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity
    {
        var entities = await options.DatabaseProvider.ReadList(expression);
        return entities;
    }
}
