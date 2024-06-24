using System.ComponentModel.DataAnnotations;

namespace lensLook.PL.ViewModel
{
    public class ResetPassViewModel
    {
        [Required(ErrorMessage = "new password is required")]
        [DataType(DataType.Password)]

        public string newpassword { get; set; }


        [Required(ErrorMessage = "confirm new password is required")]
        [DataType(DataType.Password)]
        [Compare("newpassword", ErrorMessage = "password doesn't match")]
        public string confirmnewpassword { get; set; }
    }
}
