using System.Reflection;

namespace COOrm.Library.Infrastructure.Configuration;
internal class EntityTypeBuilderMapping
{
    private static EntityTypeBuilderMapping instance;
    public static EntityTypeBuilderMapping Instance => instance ??= new EntityTypeBuilderMapping();

    private EntityTypeBuilderMapping()
    {

    }

    internal readonly Dictionary<Type, Dictionary<PropertyInfo, string>> Map = new();

    public EntityTypeBuilderMapping Create(Type type)
    {
        if (!Map.ContainsKey(type))
        {
            Map.Add(type, new Dictionary<PropertyInfo, string>());
        }

        return this;
    }

    public void AddToType(Type type, PropertyInfo prop, string columnName)
    {
        var found = Map.TryGetValue(type, out var columnMap);

        if (!found)
        {
            return;
        }

        columnMap.TryAdd(prop, columnName.ToUpperInvariant());
    }
}
