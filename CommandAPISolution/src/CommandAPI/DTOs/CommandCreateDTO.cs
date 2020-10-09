using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.DTOs
{
    public class CommandCreateDTO
    {
        [Required]
        [MaxLength(250)]
        public string   HowTo       { get; set; }

        [Required]
        public string   Platform    { get; set; }

        [Required]
        public string   CommandLine { get; set; }
    }
}
