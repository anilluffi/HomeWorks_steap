using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class Subjects
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(256)]
        public string title { get; set; }

        public DateTime? delited_at { get; set; }

    }
}
