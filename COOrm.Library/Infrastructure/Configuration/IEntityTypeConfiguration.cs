using System.Linq.Expressions;
using System.Reflection;
using COOrm.Library.Infrastructure.Base;

namespace COOrm.Library.Infrastructure.Configuration;
public interface IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    void Build(EntityTypeBuilder<TEntity> configuration);
}

public class EntityTypeBuilder<TEntity> where TEntity : BaseEntity
{
    // context.User.ToColumn(i => i.Id, "id");
    public void ToColumn<TProperty>(Expression<Func<TEntity, TProperty>> expression,
        string columnName)
    {
        var memberExp = (MemberExpression)expression.Body;
        var prop = memberExp.Member as PropertyInfo;

        var type = typeof(TEntity);

        EntityTypeBuilderMapping.Instance
                                    .Create(type)
                                    .AddToType(type, prop, columnName);
    }
}