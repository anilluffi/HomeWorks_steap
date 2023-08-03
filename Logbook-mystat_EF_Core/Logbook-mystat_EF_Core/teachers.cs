using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook_mystat_EF_Core
{
    internal class teachers
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(128)]
        public string first_name { get; set; }

        [MaxLength(128)]
        public string last_name { get; set;}


        [MaxLength(64)]
        public string email { get; set; }
    }
}
