using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first.Models
{
    public class AccountModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name is required")]
        public string First_name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Last_name { get; set;}
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Feild is required")]
        [Compare("Password", ErrorMessage = "password is not matched")]
        [DataType(DataType.Password)]
        public string Conform_password { get; set; }
        public int Phone { get; set; }
    }
}
