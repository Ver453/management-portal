using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first.Models
{
    public class MultipleFileUpload
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        [NotMapped]
        public List<IFormFile> Files { get; set; }
    }
}
