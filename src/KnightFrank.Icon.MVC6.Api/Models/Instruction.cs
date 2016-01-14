using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.Icon.MVC6.Api.Models
{
    // NOTE - This class would typically exist in the core project

    public class Instruction
    {
        public int Id { get; set; }
        public string InstructionTitle { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }

    }
}
