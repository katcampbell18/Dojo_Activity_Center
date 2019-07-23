using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class DActivity
    {
        [Key]
        public int ActivityId {get;set;}
        [Required]
        public string Title {get;set;}
        [Required]
        public string Date {get;set;}
        [RegularExpression(@"^((1[0-2]|0?[1-9]):([0-5][0-9]) ?([AaPp][Mm]))$", ErrorMessage="Time must be in 0:00am/pm format")]
        public string Time {get;set;}
        [Required]
        [Range(1, int.MaxValue, ErrorMessage="Duration must be greater than 0")]
        public int Duration {get;set;}
        [Required]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public int UserId {get;set;}
        public User Planner {get;set;}
        public List<Join> Participants {get;set;}


    }
}