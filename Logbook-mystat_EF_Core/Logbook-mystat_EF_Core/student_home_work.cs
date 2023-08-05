using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Student_Home_Work
    {
        [Key]
        [Column("home_work_id")]
        public int HomeWorkId { get; set; }
        [Key]
        [Column("student_id")]
        public int StudentId { get; set;}


        public byte status { get; set; }
        public DateTime? date_of_download { get; set; }

        [MaxLength(1024)]
        public string comment { get; set; }


        public Home_Works HomeWork { get; set; }
        public Students Student { get; set; }
    }
}
