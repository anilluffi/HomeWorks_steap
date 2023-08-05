using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Groups_Students
    {
        [Key]

        [Column("group_id")]
        public int GroupId { get; set; }

        [Key]
        [Column("student_id")]
        public int StudentId { get; set;}
        
        public byte status { get; set; }

        
        public Groups Group { get; set; }
        public Students Student { get; set; }
    }
}
