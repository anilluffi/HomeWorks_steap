using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class ClassroomsConfig : IEntityTypeConfiguration<classrooms>
    {
        public void Configure(EntityTypeBuilder<classrooms> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.title).HasMaxLength(128);

        }

    }

    internal class Classrooms_statusesConfig : IEntityTypeConfiguration<classrooms_statuses>
    {
        public void Configure(EntityTypeBuilder<classrooms_statuses> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");

        }

    }

    internal class GroupsConfig : IEntityTypeConfiguration<groups>
    {
        public void Configure(EntityTypeBuilder<groups> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.title).HasMaxLength(256);
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(1);

        }

    }

    internal class groups_start_finishConfig : IEntityTypeConfiguration<groups_start_finish>
    {
        public void Configure(EntityTypeBuilder<groups_start_finish> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");

        }

    }



    internal class Groups_studentsConfig : IEntityTypeConfiguration<groups_students>
    {
        public void Configure(EntityTypeBuilder<groups_students> builder)
        {
            builder.HasKey(u => new { u.group_id, u.student_id });

        }

    }

    internal class Home_WorksConfig : IEntityTypeConfiguration<home_works>
    {
        public void Configure(EntityTypeBuilder<home_works> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.theme).HasMaxLength(128);
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(0);

        }

    }


    internal class Pair_CrystalsConfig : IEntityTypeConfiguration<pair_crystals>
    {
        public void Configure(EntityTypeBuilder<pair_crystals> builder)
        {
            builder.HasKey(u => new { u.pair_id, u.student_id });
            builder.ToTable(t => t.HasCheckConstraint("quantity", "(quantity >= 1 AND quantity <= 3)"));

        }

    }

    internal class PairsConfig : IEntityTypeConfiguration<pairs>
    {
        public void Configure(EntityTypeBuilder<pairs> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.online_status).HasColumnName("online_status").HasDefaultValue(0);
            builder.Property(u => u.theme).HasMaxLength(128);

        }

    }

    internal class Pairs_StudentsConfig : IEntityTypeConfiguration<pairs_students>
    {
        public void Configure(EntityTypeBuilder<pairs_students> builder)
        {
            builder.HasKey(u => new { u.pair_id, u.student_id });
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(0);
            builder.Property(u => u.is_online).HasColumnName("is_online").HasDefaultValue(0);
            builder.Property(u => u.comment).HasMaxLength(1024);


            builder.HasCheckConstraint("CK_pairs_students_status", "status IN (0, 1, 2, 3)");
            builder.HasCheckConstraint("CK_pairs_students_grade", "grade BETWEEN 1 AND 12");

        }

    }

    
    internal class Schedule_ItemConfig : IEntityTypeConfiguration<schedule_item>
    {
        public void Configure(EntityTypeBuilder<schedule_item> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(1);
        }

    }

    internal class Student_Home_WorkConfig : IEntityTypeConfiguration<student_home_work>
    {
        public void Configure(EntityTypeBuilder<student_home_work> builder)
        {
            builder.HasKey(u => new { u.home_work_id, u.student_id });
            builder.Property(u => u.comment).HasMaxLength(1024);

            builder.HasCheckConstraint("CK_student_home_work_status", "status IN(1, 2, 3, 4, 5, 6)");
        }

    }


    internal class studentsConfig : IEntityTypeConfiguration<students>
    {
        public void Configure(EntityTypeBuilder<students> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.first_name).HasMaxLength(128);
            builder.Property(u => u.last_name).HasMaxLength(128);
            builder.Property(u => u.email).HasMaxLength(64);

        }

    }

    internal class subjectsConfig : IEntityTypeConfiguration<subjects>
    {
        public void Configure(EntityTypeBuilder<subjects> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.title).HasMaxLength(256);

        }

    }


    internal class subjects_teachersConfig : IEntityTypeConfiguration<subjects_teachers>
    {
        public void Configure(EntityTypeBuilder<subjects_teachers> builder)
        {
            builder.HasKey(u => new { u.subject_id, u.teacher_id });

        }

    }

    internal class teachersConfig : IEntityTypeConfiguration<teachers>
    {
        public void Configure(EntityTypeBuilder<teachers> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.first_name).HasMaxLength(128);
            builder.Property(u => u.last_name).HasMaxLength(128);
            builder.Property(u => u.email).HasMaxLength(64);

        }

    }

}
