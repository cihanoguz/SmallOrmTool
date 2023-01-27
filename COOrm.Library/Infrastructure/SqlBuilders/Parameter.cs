
namespace COOrm.Infrastructure.SqlBuilders;
internal class Parameter
{
    public string Name { get; set; }

    public object Value { get; set; }
    public Parameter(string name, object value)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }
}
