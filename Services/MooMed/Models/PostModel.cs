using System;

namespace MooMed.Web.Models
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