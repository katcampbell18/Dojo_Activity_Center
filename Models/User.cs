using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters or longer")]
        [Display(Name = "First Name")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters or longer")]
        [Display(Name = "Last Name")]
        public string LastName {get;set;}
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain at least 1 number, 1 letter, 1 special character, and be at least 8 characters long")]
        [DataType(DataType.Password)]
        [Required]
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public List<DActivity> CreatedActivity {get;set;}
        public List<Join> Participating {get;set;}
    }
}