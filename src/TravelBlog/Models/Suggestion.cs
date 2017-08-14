using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlog.Models
{
    [Table("Suggestions")]
    public class Suggestion
    {
        [Key]
        public int SuggestionId { get; set; }
        public bool Visted { get; set; }
        public int UpVote { get; set; }
        public string Details { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Location Location { get; set; }


    }
}
