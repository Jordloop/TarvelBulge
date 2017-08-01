using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table ("Experiences")]
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }
        public string Story { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
