using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class groups_start_finish
    {
        [Column("id")]
        public int Id { get; set; }
        public DateTime start { get; set; }
        public DateTime? finish { get; set; }
        public int group_id { get; set; }

        public groups groups { get; set; }

    }
}
