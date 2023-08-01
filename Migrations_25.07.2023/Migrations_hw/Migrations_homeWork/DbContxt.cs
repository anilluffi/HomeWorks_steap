using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations_homeWork
{
    internal class DbContxt : DbContext
    {
        public DbSet<users> users { get; set; }

        public DbContxt(DbContextOptions<DbContxt> options) :
            base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
                builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=p01_migrations;Trusted_Connection=True;Encrypt=False;");
        }
    }
}
