using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{

    internal class Schedule_item
    {
        [Column("id")]
        public int Id { get; set; }
        public byte number { get; set; }
        public DateTime pair_start { get; set; }
        public DateTime pair_end { get; set; }

        [DefaultValue(1)]
        public byte status { get; set; }


    }
}
