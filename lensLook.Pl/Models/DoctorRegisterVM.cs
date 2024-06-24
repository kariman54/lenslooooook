using System.ComponentModel.DataAnnotations;

namespace lensLook.Pl.Models
{
    public class DoctorRegisterVM
    {
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Rquired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Rquired")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn't match")]
        public string ConfirmPassword { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }


    }
}
