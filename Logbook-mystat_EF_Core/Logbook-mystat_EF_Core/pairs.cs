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
    internal class pairs
    {
        [Column("id")]
        public int Id { get; set; }
        public DateTime pair_date { get; set; }

        [DefaultValue(false)]
        public bool online_status { get; set; }
        [MaxLength(128)]
        public string theme { get; set; }
        public int schedule_item_id { get; set;}
        public int subject_id { get; set; }
        public int group_id { get; set; }
        public int classroom_id { get; set; }
        public int teacher_id { get; set; }

        public schedule_item schedule_item { get; set; }
        public subjects subject { get; set; }
        public groups group { get; set; }
        public classrooms classrooms { get; set; }
        public teachers teacher { get; set; }

    }

}
