using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Migrations_homeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations_homeWork
{
    internal class DbCtxFactory : IDesignTimeDbContextFactory<DbContxt>
    {
        public DbContxt CreateDbContext(string[] args)
        {
            ConfigurationBuilder cBuilder = new ConfigurationBuilder();
            cBuilder.SetBasePath(Directory.GetCurrentDirectory());

            cBuilder.AddJsonFile("config.json");
            var config = cBuilder.Build();

            string? connString = config.GetConnectionString("Express");


            DbContextOptionsBuilder<DbContxt> builder = new DbContextOptionsBuilder<DbContxt>();
            builder.UseSqlServer(connString);
            DbContextOptions<DbContxt> options = builder.Options;

            return new DbContxt(options);
        }
    }
}
