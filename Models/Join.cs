using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class Join
    {
        [Key]
        public int JoinId {get;set;}
        public int UserId {get;set;}
        public int ActivityId {get;set;}
        public User User {get;set;}
        public DActivity Activity {get;set;}
    }
}