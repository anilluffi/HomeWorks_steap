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
    internal class Pairs_Students
    {
        [Key]
        [Column("pair_id")]
        public int PairId { get; set; }
        [Key]
        [Column("student_id")]
        public int StudentId { get; set;}

        [DefaultValue(0)]
        public byte status { get; set; }

        [DefaultValue(0)]
        public bool is_online { get; set; }

        public byte grade { get; set; }

        [MaxLength(1024)]
        public string? comment { get; set; }

        public Pairs Pair { get; set; }
        public Students Student { get; set; }


    }
}
