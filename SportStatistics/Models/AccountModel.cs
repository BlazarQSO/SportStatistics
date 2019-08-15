using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity; 

namespace SportStatistics.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("name = UsersConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } 
    }

    public class LoginModel
    {
        [MaxLength(20, ErrorMessage = "Username cannot be longer than 20 characters")]
        [MinLength(3, ErrorMessage = "Username cannot be shorter than 3 characters")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5 characters")]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }

    public class RegistrationModel
    {
        [MaxLength(20, ErrorMessage = "Username cannot be longer than 20 characters")]
        [MinLength(3, ErrorMessage = "Username cannot be shorter than 3 characters")]
        public string Login { get; set; }

        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5 characters")]
        [MaxLength(50, ErrorMessage = "Password must not exceed 50 characters")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords did not match")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter Email")]    
        public string Email { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Enter password")]
        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5 characters")]
        [MaxLength(50, ErrorMessage = "Password must not exceed 50 characters")]
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5 characters")]
        [MaxLength(100, ErrorMessage = "Password must not exceed 50 characters")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords did not match")]
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }

    public class ChangeNameModel
    {
        [Required(ErrorMessage = "Enter new name")]
        [MaxLength(20, ErrorMessage = "Username cannot be longer than 20 characters")]
        [MinLength(3, ErrorMessage = "Username cannot be shorter than 3 characters")]
        public string NewName { get; set; }

        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5 characters")]
        [MaxLength(50, ErrorMessage = "Password must not exceed 50 characters")]
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords did not match")]
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Enter username")]
        [MaxLength(20, ErrorMessage = "Username cannot be longer than 20 characters")]
        [MinLength(3, ErrorMessage = "Username cannot be shorter than 3 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}