using System.ComponentModel.DataAnnotations;

namespace lensLook.Pl.Models
{
	public class ForgetPasswordVM
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }



	}
}
