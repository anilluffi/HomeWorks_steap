using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Classrooms
    {
        [Column("id")]
        public int Id { get; set; }
        public byte number { get; set; }

        [MaxLength(128)]
        public string title { get; set; }
        [Column("classroom_statuse_id")]
        public int  ClassroomStatusId { get; set; }

        public Classrooms_Statuses ClassroomStatus { get; set; }

    }
}
