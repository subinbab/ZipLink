using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class URLDTO
    {
        [Required]
        public int id { get; set; }
        public string url { get; set; }
        public string generatedUrl { get; set; }
        public string shortenUrl { get; set; }
        public string createdby { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;
        public string modifieddby { get; set; }
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}
