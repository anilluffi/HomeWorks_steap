using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Groups_Start_Finish
    {
        [Column("id")]
        public int Id { get; set; }
        public DateTime start { get; set; }
        public DateTime? finish { get; set; }
        public int group_id { get; set; }

        public Groups groups { get; set; }

    }
}
