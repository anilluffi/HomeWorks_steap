using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class groups_students
    {
        [Key]
        public int group_id { get; set; }

        [Key]
        public int student_id { get; set;}
        public byte status { get; set; }

        public groups groups { get; set; }

        public students students { get; set; }
    }
}
