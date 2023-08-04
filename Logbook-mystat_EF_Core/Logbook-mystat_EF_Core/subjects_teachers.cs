using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Subjects_Teachers
    {
        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        [Key]
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        [Column("teachers")]
        public Teachers Teacher { get; set; }

        [Column("subjects")]
        public Subjects Subject { get; set; } 

    }
}
