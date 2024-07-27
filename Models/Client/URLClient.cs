using Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Client
{
    public class URLClient
    {
        [Required]
        [RegularExpression(@"^(https?:\/\/)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/[^\s]*)?$", ErrorMessage = "Please enter a valid URL.")]
        public string url { get; set; }
        public string clientIp { get; set; }
        public string generatedUrl { get; set; }
        public string shortenurl { get; set; }
        public string userId { get; set; }
        public int id { get; set; }
        public ApplicationUser appUser { get; set; }
        public string imageUrl { get; set; }
        public string text { get; set; }
    }
}
