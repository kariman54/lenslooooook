using System.ComponentModel.DataAnnotations;

namespace lensLook.Pl.Models
{
	public class LoginVM
	{
		[Required(ErrorMessage = "This Column Is Required")]
		[EmailAddress(ErrorMessage = "Enter Email Valid ")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "This Column Is Required")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }

	}
}
