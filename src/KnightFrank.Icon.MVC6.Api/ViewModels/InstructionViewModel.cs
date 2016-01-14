using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.Icon.MVC6.Api.ViewModels
{
    public class InstructionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string InstructionTitle { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        [Required]
        public string Town { get; set; }

        public string County { get; set; }

        [Required]
        public string Postcode { get; }
    }
}
