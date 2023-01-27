using COOrm.Console;
using COOrm.Library.DbContext;
using COOrm.Library.Infrastructure.Configuration;
using COOrm.Library.Interfaces.DatabaseProviders;


var context = new COContext(opt =>
{
    opt.UseSqlServer("yoursqlconn");

    //opt.AddEntity<User>();

    opt.AddEntityConfiguration(new UserConfiguration());
});

var user = new User()
{ 
    Id = new Random().Next(1, 101),
    UserName = "CO"
};

var result = await context.Insert(user);

var users = await context.ReadList<User>();


Console.ReadLine();


class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Build(EntityTypeBuilder<User> configuration)
    {
        configuration.ToColumn(i => i.Id, "id");
        configuration.ToColumn(i => i.UserName, "username");
    }
}
