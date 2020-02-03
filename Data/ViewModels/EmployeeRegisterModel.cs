using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class EmployeeRegisterModel
    {
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Đội nhóm")]
        public int TeamId { get; set; }

        [Display(Name = "Vai trò")]
        public List<string> Roles { get; set; }


        public bool haveAccount { get; set; }
    }
}
