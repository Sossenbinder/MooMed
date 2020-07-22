using System;

namespace MooMed.Frontend.Models
{
    public class PostModel
    {   
        public Guid PostId { get; set; }

        public string PostContent { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}