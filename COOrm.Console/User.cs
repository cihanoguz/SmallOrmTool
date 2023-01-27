using COOrm.Library.Infrastructure.Attributes;
using COOrm.Library.Infrastructure.Base;

namespace COOrm.Console;

[TableName("USER", "dbo")]
internal class User: BaseEntity
{
    [ColumnName("id")]
    public int Id { get; set; }

    [ColumnName("userName")]
    public string UserName { get; set; }
}
