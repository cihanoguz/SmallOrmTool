using COOrm.Library.Infrastructure.Base;
using COOrm.Library.Interfaces.ColumnNameProviders;
using COOrm.Library.Interfaces.TableNameProviders;

namespace COOrm.Library.Infrastructure.Mapping;
internal class Mapping
{
    private static Mapping instance;
    public static Mapping Instance => instance ??= new Mapping();

    private Dictionary<string, ObjectMap> maps;

    private Mapping()
    {
        maps = new();
    }

    public ObjectMap Get<TEntity>() where TEntity : BaseEntity
    {
        return Get(typeof(TEntity));
    }

    internal ObjectMap Get(Type type)
    {
        maps.TryGetValue(type.Name, out var map);

        return map;
    }

    internal IEnumerable<string> GetColumnNames<TEntity>() where TEntity : BaseEntity
    {
        maps.TryGetValue(typeof(TEntity).Name, out var map);
        return map?.ColumnNames;
    }

    internal void SetMapping(ObjectMap objectMap)
    {
        var entityTypeName = objectMap.Entity.Name;

        if (maps.ContainsKey(entityTypeName))
            maps[entityTypeName] = objectMap;
        else
            maps.Add(entityTypeName, objectMap);
    }

    internal ObjectMap SetMapping<TEntity>(ITableNameProvider tableNameProvider,
                                           IColumnNameProvider columnNameProvider) where TEntity : BaseEntity
    {
        var map = new ObjectMap(typeof(TEntity))
        {
            TableName = tableNameProvider.GetTableName<TEntity>(),
            ColumnNamePropertyMap = columnNameProvider.CreateColumnMap(typeof(TEntity))
        };

        SetMapping(map);

        return map;
    }

    internal ObjectMap SetMapping(Type type,
                                  ITableNameProvider tableNameProvider,
                                  IColumnNameProvider columnNameProvider)
    {
        var map = new ObjectMap(type)
        {
            TableName = tableNameProvider.GetTableName(type),
            ColumnNamePropertyMap = columnNameProvider.CreateColumnMap(type)
        };

        SetMapping(map);

        return map;
    }

}
