
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace first.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="This feild is required")]
        [DisplayName("Student Name")]
        public string Student_name { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Student Description*")]
        public string Student_description { get; set; }
        [Required(ErrorMessage ="Please enter a gender!")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        public Boolean Enabled { get; set; }
        public string imgUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Faculty { get; set; }

        [ForeignKey("faculty_id")]
        public virtual  Faculty faculties { get; set; }
        
    }
}
