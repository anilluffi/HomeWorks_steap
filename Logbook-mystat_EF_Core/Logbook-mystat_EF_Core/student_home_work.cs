﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Student_Home_Work
    {
        [Key]
        public int home_work_id { get; set; }
        [Key]
        public int student_id { get; set;}


        public byte status { get; set; }
        public DateTime? date_of_download { get; set; }

        [MaxLength(1024)]
        public string comment { get; set; }


        public Home_Works home_works { get; set; }
        public Students students { get; set; }
    }
}
