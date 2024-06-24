using System.ComponentModel.DataAnnotations;

namespace lensLook.Pl.Models
{
	public class RegisterVM
	{

        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        //public string Gender { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Doesn't match")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }



        #region Property of Doctor

        public int? Experience { get; set; }

        public string? Specialization { get; set; }
        public string? MedicalLicenseNumber { get; set; }

        public string? HomeAddress { get; set; }
        public IFormFile? Image { get; set; }

        public int? NumberOfAppointments { get; set; }
        #endregion



    }
}
