using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Pairs
    {
        [Column("id")]
        public int Id { get; set; }
        public DateTime pair_date { get; set; }

        [DefaultValue(false)]
        public bool online_status { get; set; }
        [MaxLength(128)]
        public string theme { get; set; }

        [Column("schedule_item_id")]
        public int ScheduleItemId  { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        [Column("classroom_id")]
        public int ClassroomId { get; set; }

        [Column("teacher_id")]
        public int TeacherId { get; set; }

        [Column("schedule_item")]
        public Schedule_item ScheduleItem  { get; set; }

        [Column("subject")]
        public Subjects Subject { get; set; }

        [Column("group")]
        public Groups Group { get; set; }

        [Column("classrooms")]
        public Classrooms Classroom { get; set; }

        [Column("teacher")]
        public Teachers Teacher { get; set; }

    }

}
