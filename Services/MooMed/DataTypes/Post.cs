using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Web.DataTypes
{
    [Table("Posts")]
    public class PostEntity
    {
        [Key]
        [Column("PostId")]
        public Guid PostId { get; set; }

        [Column("PostContent")]
        public string PostContent { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Column("User")]
        public Account User { get; set; }
    }
}