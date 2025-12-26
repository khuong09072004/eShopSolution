using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Ten")]
        public string FirstName { set; get; }
        [Display(Name ="Ho")]
        public string LastName { set; get; }
        [Display(Name ="Ngay Sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { set; get; }
        [Display(Name ="Hom Thu")]
        public string Email { set; get; }
        [Display(Name = "So dien thoai")]
        public string PhoneNumber { set; get; }
        [Display(Name = "Tai khoan")]
        public string UserName { set; get; }
        [Display(Name = "Mat khau")]
        public string Password { set; get; }
        [Display(Name = "Xac nhan mat khau")]
        public string ConfirmPassword { set; get; }

    }
}
