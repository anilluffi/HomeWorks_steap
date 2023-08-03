using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class dbContxt : DbContext
    {
        public DbSet<schedule_item> schedule_item { get; set; }
        public DbSet<subjects> subjects { get; set; }
        public DbSet<groups> groups { get; set; }
        public DbSet<classrooms_statuses> classrooms_statuses { get; set; }

        public DbSet<classrooms> classrooms { get; set; }

        public DbSet<teachers> teachers { get; set; }
        public DbSet<pairs> pairs { get; set; }
        public DbSet<subjects_teachers> subjects_teachers { get; set; }
        public DbSet<students> students { get; set; }
        public DbSet<groups_students> groups_students { get; set; }
        public DbSet<pairs_students> pairs_students { get; set; }




        public dbContxt()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=p01_fluent_api_attr;Trusted_Connection=True;Encrypt=False;");
        }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            //  mb.Entity<Role>();

            //  mb.Ignore(typeof(Role));
            //  mb.Ignore<Role>();

            //  mb.Entity<User>().Ignore(u => u.Token);

            //  mb.Entity<User>().ToTable("clients");

            //  mb.Entity<User>().Property(u => u.Id).HasColumnName("id");

            // mb.Entity<User>().Property(u => u.Id).HasField("id");
            // mb.Entity<User>().Property(u => u.Email).HasField("email");
            // mb.Entity<User>().Property(u => u.Age).HasField("age");
            // mb.Entity<User>().Property("hash");

            // mb.Entity<User>().Property(u => u.Age).IsRequired();

            // mb.Entity<User>().HasKey(u => u.Ident).HasName("PK_users_777");

            // mb.Entity<User>().HasKey(u => new { u.Email, u.Nickname});

            // mb.Entity<User>().HasAlternateKey(u => u.Email);

            // mb.Entity<User>().HasAlternateKey(u => new { u.Email, u.Age});

            // mb.Entity<User>().HasIndex(u => u.Email).IsUnique().HasName("IX_users_email_unique");

            // mb.Entity<User>().Property(u => u.Id).ValueGeneratedNever();

            // mb.Entity<User>().Property(u => u.Age).HasDefaultValue(18);

            // mb.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

            // mb.Entity<User>().Property(u => u.CreatedAt).HasComputedColumnSql("FirstName + ' ' + LastName");

            // mb.Entity<User>().ToTable(t => t.HasCheckConstraint("Password", "LEN(Password) > 8"));

            // mb.Entity<User>().Property(u => u.Password).HasMaxLength(20);

            //mb.ApplyConfiguration(new UserConfig());

        }



    }
}
