using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first.Models
{
    public class Faculty
    {
        [Key]
        public int faculty_id { get; set; }
        [Required]
        [DisplayName("Faculty Name")]
        public string faculty_name { get; set; }
    }
}
