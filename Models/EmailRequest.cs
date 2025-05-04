using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Models
{
    public class EmailRequest
    {
        [EmailAddress]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }
}
