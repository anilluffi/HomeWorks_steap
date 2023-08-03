using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class subjects_teachers
    {
        [Key]
        public int subject_id { get; set; }
        [Key]
        public int teacher_id { get; set; }

        public teachers teachers { get; set; }
        public subjects subjects { get; set; } 

    }
}
