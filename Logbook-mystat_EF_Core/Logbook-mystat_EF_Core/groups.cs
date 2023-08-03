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
    internal class groups
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(256)]
        public string title { get; set; }

        [DefaultValue((byte)1)]
        public byte status { get; set; }

        public int groups_start_finish_id { get; set; }
        public groups_start_finish groups_start_finish { get; set; }
    }
}
