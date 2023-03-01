using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Models
{
	public class ResetPasswordModel
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string Token { get; set; }

		[Display(Name = "New Password")]
		[Required(ErrorMessage = "Please enter a new strong password")]
		[DataType(DataType.Password)]
		public string NewPassword { set; get; }

		[Display(Name = "Confirm New Password")]
		[Required(ErrorMessage = "Please confirm the new password")]
		[Compare("NewPassword", ErrorMessage = "Password does not match")]
		[DataType(DataType.Password)]
		public string ConfirmNewPassword { set; get; }

		public bool IsSuccess { get; set; }
	}
}
