using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations_hw
{
    internal class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? biography { get; set; }
        public char? Gender { get; set; }
    }
}
