using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lensLook.Pl.Models
{
	public class UserVM
	{
		public string id { get; set; }

		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }

		[RegularExpression("^(?:\\+20|0)?1[0-9]{9}$")]
		public string PhoneNumber { get; set; }

		public IEnumerable<string> Role { get; set; }


		public UserVM()
		{
			//id=Guid.NewGuid().ToString();
		}


	}
}
