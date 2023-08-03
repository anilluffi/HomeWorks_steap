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
    internal class home_works
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(128)]
        public string theme { get; set; }
        public DateTime start { get; set; }
        public DateTime? finish { get; set; }
        public int pair_id { get; set; }
        public int teacher_id { get; set; }
        public int group_id { get; set; }

        [DefaultValue((byte)0)]
        public byte status { get; set; }

        public pairs pairs { get; set; }
        public teachers teachers { get; set; }
        public groups groups { get; set; }


    }
}
