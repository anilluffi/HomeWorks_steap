using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Logbook_mystat_EF_Core
{
    internal class ClassroomsConfig : IEntityTypeConfiguration<Classrooms>
    {
        public void Configure(EntityTypeBuilder<Classrooms> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.title).HasMaxLength(128);

            builder.Property(u => u.ClassroomStatusId).HasColumnName("classroom_statuse_id");

            builder.HasOne(c => c.ClassroomStatus)  
               .WithMany()                    
               .HasForeignKey(c => c.ClassroomStatusId)  
               .OnDelete(DeleteBehavior.Restrict);
        }

    }

    internal class Classrooms_StatusesConfig : IEntityTypeConfiguration<Classrooms_Statuses>
    {
        public void Configure(EntityTypeBuilder<Classrooms_Statuses> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");

        }

    }

    internal class GroupsConfig : IEntityTypeConfiguration<Groups>
    {
        public void Configure(EntityTypeBuilder<Groups> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.title).HasMaxLength(256);
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(1);

        }

    }

    internal class Groups_Start_FinishConfig : IEntityTypeConfiguration<Groups_Start_Finish>
    {
        public void Configure(EntityTypeBuilder<Groups_Start_Finish> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");

        }

    }



    internal class Groups_StudentsConfig : IEntityTypeConfiguration<Groups_Students>
    {
        public void Configure(EntityTypeBuilder<Groups_Students> builder)
        {
            builder.HasKey(u => new { u.GroupId, u.StudentId });

        }

    }

    internal class Home_WorksConfig : IEntityTypeConfiguration<Home_Works>
    {
        public void Configure(EntityTypeBuilder<Home_Works> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.theme).HasMaxLength(128);
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(0);

        }

    }


    internal class Pair_CrystalsConfig : IEntityTypeConfiguration<Pair_Crystals>
    {
        public void Configure(EntityTypeBuilder<Pair_Crystals> builder)
        {
            builder.HasKey(u => new { u.pair_id, u.student_id });
            builder.ToTable(t => t.HasCheckConstraint("quantity", "(quantity >= 1 AND quantity <= 3)"));

        }

    }

    internal class PairsConfig : IEntityTypeConfiguration<Pairs>
    {
        public void Configure(EntityTypeBuilder<Pairs> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.online_status).HasColumnName("online_status").HasDefaultValue(0);
            builder.Property(u => u.theme).HasMaxLength(128);


            builder.HasOne(p => p.ScheduleItem)
            .WithMany()
            .HasForeignKey(p => p.ScheduleItemId)
            .HasConstraintName("FK_rairs_schedule_item");

            builder.Property(u => u.ScheduleItemId).HasColumnName("schedule_item_id");
            builder.Property(u => u.ScheduleItem).HasColumnName("schedule_item");


            builder.HasOne(p => p.Subject)
                .WithMany()
                .HasForeignKey(p => p.SubjectId)
                .HasConstraintName("FK_rairs_subject");

            builder.Property(u => u.SubjectId).HasColumnName("subject_id");
            builder.Property(u => u.Subject).HasColumnName("subject");


            builder.HasOne(p => p.Group)
                .WithMany()
                .HasForeignKey(p => p.GroupId)
                .HasConstraintName("FK_rairs_group");

            builder.Property(u => u.GroupId).HasColumnName("group_id");
            builder.Property(u => u.Group).HasColumnName("group");


            builder.HasOne(p => p.Classroom)
                .WithMany()
                .HasForeignKey(p => p.ClassroomId)
                .HasConstraintName("FK_rairs_classroom");

            builder.Property(u => u.ClassroomId).HasColumnName("classroom_id");
            builder.Property(u => u.Classroom).HasColumnName("classroom");



            builder.HasOne(p => p.Teacher)
                .WithMany()
                .HasForeignKey(p => p.TeacherId)
                .HasConstraintName("FK_rairs_teacher");

            builder.Property(u => u.TeacherId).HasColumnName("teacher_id");
            builder.Property(u => u.Teacher).HasColumnName("teacher");
        }

    }

    internal class Pairs_StudentsConfig : IEntityTypeConfiguration<Pairs_Students>
    {
        public void Configure(EntityTypeBuilder<Pairs_Students> builder)
        {
            builder.HasKey(u => new { u.pair_id, u.student_id });
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(0);
            builder.Property(u => u.is_online).HasColumnName("is_online").HasDefaultValue(0);
            builder.Property(u => u.comment).HasMaxLength(1024);


            builder.HasCheckConstraint("CK_pairs_students_status", "status IN (0, 1, 2, 3)");
            builder.HasCheckConstraint("CK_pairs_students_grade", "grade BETWEEN 1 AND 12");

        }

    }

    
    internal class Schedule_ItemConfig : IEntityTypeConfiguration<Schedule_item>
    {
        public void Configure(EntityTypeBuilder<Schedule_item> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.status).HasColumnName("status").HasDefaultValue(1);
        }

    }

    internal class Student_Home_WorkConfig : IEntityTypeConfiguration<Student_Home_Work>
    {
        public void Configure(EntityTypeBuilder<Student_Home_Work> builder)
        {
            builder.HasKey(u => new { u.home_work_id, u.student_id });
            builder.Property(u => u.comment).HasMaxLength(1024);

            builder.HasCheckConstraint("CK_student_home_work_status", "status IN(1, 2, 3, 4, 5, 6)");
        }

    }


    internal class StudentsConfig : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.first_name).HasMaxLength(128);
            builder.Property(u => u.last_name).HasMaxLength(128);
            builder.Property(u => u.email).HasMaxLength(64);

        }

    }

    internal class SubjectsConfig : IEntityTypeConfiguration<Subjects>
    {
        public void Configure(EntityTypeBuilder<Subjects> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.title).HasMaxLength(256);

        }

    }


    internal class Subjects_TeachersConfig : IEntityTypeConfiguration<Subjects_Teachers>
    {
        public void Configure(EntityTypeBuilder<Subjects_Teachers> builder)
        {
            builder.HasKey(u => new { u.SubjectId, u.TeacherId });

            builder.HasOne(st => st.Subject)
            .WithMany()
            .HasForeignKey(st => st.SubjectId)
            .HasConstraintName("FK_subjects_teachers_subject");

            builder.Property(u => u.SubjectId).HasColumnName("subject_id");
            builder.Property(u => u.Subject).HasColumnName("subject");


            builder.HasOne(st => st.Teacher)
                .WithMany()
                .HasForeignKey(st => st.TeacherId)
                .HasConstraintName("FK_subjects_teachers_teacher");

            builder.Property(u => u.TeacherId).HasColumnName("teacher_id");
            builder.Property(u => u.Teacher).HasColumnName("teacher");

        }

    }

    internal class TeachersConfig : IEntityTypeConfiguration<Teachers>
    {
        public void Configure(EntityTypeBuilder<Teachers> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.first_name).HasMaxLength(128);
            builder.Property(u => u.last_name).HasMaxLength(128);
            builder.Property(u => u.email).HasMaxLength(64);

        }

    }

}
