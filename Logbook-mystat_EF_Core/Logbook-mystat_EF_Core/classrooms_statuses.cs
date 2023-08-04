using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Classrooms_Statuses
    {
        [Column("id")]
        public int Id { get; set; }
        public byte status { get; set; }
        public DateTime time_start { get; set; }
        public DateTime time_end { get; set; }
    }
}
