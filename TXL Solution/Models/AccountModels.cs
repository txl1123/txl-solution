using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TxlMvc.Models
{
   
        public class UsersContext : DbContext
        {
            //public UsersContext()
            //    : base("DefaultConnection")
            //{
            //}

            public DbSet<UserProfile> UserProfiles { get; set; }
        }
        [Table("UserProfile")]
        public class UserProfile
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }
        }

        public class LoginModel
        {
            [Required]
            [Display(Name = "用户名")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            
        }

}