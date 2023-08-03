using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class classrooms
    {
        [Column("id")]
        public int Id { get; set; }
        public byte number { get; set; }

        [MaxLength(128)]
        public string title { get; set; }
        public int classroom_statuse_id { get; set; }

        public classrooms_statuses ClassroomStatus { get; set; }

    }
}
