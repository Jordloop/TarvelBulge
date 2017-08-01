using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table ("People")]
    public class People
    {
        [Key]
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public int ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }
    }
}
