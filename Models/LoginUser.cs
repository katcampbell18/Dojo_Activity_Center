using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class LoginUser
    {
        [Display(Name="Email")]
        public string LogEmail {get;set;}
        [Display(Name="Password")]
        public string LogPassword {get;set;}
    }
}