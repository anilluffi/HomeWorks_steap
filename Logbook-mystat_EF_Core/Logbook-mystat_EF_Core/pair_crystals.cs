using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Pair_Crystals
    {
        [Key]
        [Column("pair_id")]
        public int PairId { get; set; }
        [Key]
        [Column("student_id")]
        public int StudentId { get; set;}
        public byte quantity { get; set; }

        public Pairs Pair { get; set; }
        public Students Student { get; set; }

    }
}
