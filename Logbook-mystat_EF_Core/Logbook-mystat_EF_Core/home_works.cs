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
    internal class Home_Works
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(128)]
        public string theme { get; set; }
        public DateTime start { get; set; }
        public DateTime? finish { get; set; }

        [Column("pair_id")]
        public int PairId { get; set; }


        [Column("teacher_id")]
        public int TeacherId { get; set; }


        [Column("group_id")]
        public int GroupId { get; set; }

        [DefaultValue((byte)0)]
        public byte status { get; set; }

        public Pairs Pair { get; set; }
        public Teachers Teacher { get; set; }
        public Groups Group { get; set; }


    }
}
