using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Pairs_Students
    {
        [Key]
        public int pair_id { get; set; }
        [Key]
        public int student_id { get; set;}

        [DefaultValue(0)]
        public byte status { get; set; }

        [DefaultValue(0)]
        public bool is_online { get; set; }

        public byte grade { get; set; }

        [MaxLength(1024)]
        public string? comment { get; set; }

        public Pairs pairs { get; set; }
        public Students students { get; set; }


    }
}
