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
        public DbSet<Schedule_item> schedule_item { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<Groups> groups { get; set; }
        public DbSet<Classrooms_Statuses> classrooms_statuses { get; set; }

        public DbSet<Classrooms> classrooms { get; set; }

        public DbSet<Teachers> teachers { get; set; }
        public DbSet<Pairs> pairs { get; set; }
        public DbSet<Subjects_Teachers> subjects_teachers { get; set; }
        public DbSet<Students> students { get; set; }
        public DbSet<Groups_Students> groups_students { get; set; }
        public DbSet<Pairs_Students> pairs_students { get; set; }




        public dbContxt()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=p01_codefirs;Trusted_Connection=True;Encrypt=False;");
        }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new ClassroomsConfig());
            mb.ApplyConfiguration(new Classrooms_StatusesConfig());
            mb.ApplyConfiguration(new GroupsConfig());
            mb.ApplyConfiguration(new Groups_Start_FinishConfig());
            mb.ApplyConfiguration(new Groups_StudentsConfig());
            mb.ApplyConfiguration(new Home_WorksConfig());
            mb.ApplyConfiguration(new Pair_CrystalsConfig());
            mb.ApplyConfiguration(new PairsConfig());
            mb.ApplyConfiguration(new Pairs_StudentsConfig());
            mb.ApplyConfiguration(new Schedule_ItemConfig());
            mb.ApplyConfiguration(new Student_Home_WorkConfig());
            mb.ApplyConfiguration(new StudentsConfig());
            mb.ApplyConfiguration(new SubjectsConfig());
            mb.ApplyConfiguration(new Subjects_TeachersConfig());
            mb.ApplyConfiguration(new TeachersConfig());

        }



    }
}
