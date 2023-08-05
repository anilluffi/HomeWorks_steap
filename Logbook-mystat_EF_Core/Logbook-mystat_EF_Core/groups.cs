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
    internal class Groups
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(256)]
        public string title { get; set; }

        [DefaultValue((byte)1)]
        public byte status { get; set; }

        public int groups_start_finish_id { get; set; }



        [Column("groups_start_finish")]
        public Groups_Start_Finish GroupsStartFinish  { get; set; }
        public ICollection<Groups_Students> Students_Groups { get; set; }

    }
}
