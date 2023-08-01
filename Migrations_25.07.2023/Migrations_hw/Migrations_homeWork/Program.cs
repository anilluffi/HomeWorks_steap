using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Migrations_homeWork;




ConfigurationBuilder cBuilder = new ConfigurationBuilder();
cBuilder.SetBasePath(Directory.GetCurrentDirectory());

cBuilder.AddJsonFile("config.json");
var config = cBuilder.Build();

string? connString = config.GetConnectionString("Express");

DbContextOptionsBuilder<DbContxt> builder = new DbContextOptionsBuilder<DbContxt>();
builder.UseSqlServer(connString);
DbContextOptions<DbContxt> options = builder.Options;



using DbContxt dbc = new DbContxt(options);
await dbc.Database.MigrateAsync();