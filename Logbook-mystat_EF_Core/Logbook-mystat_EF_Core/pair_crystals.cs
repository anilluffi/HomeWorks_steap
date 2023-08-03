using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class pair_crystals
    {
        [Key]
        [Column(Order = 1)]
        public int pair_id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int student_id { get; set;}
        public byte quantity { get; set; }

        public pairs pairs { get; set; }
        public students students { get; set; }

    }
}
