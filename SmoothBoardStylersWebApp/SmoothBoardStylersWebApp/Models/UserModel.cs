using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmoothBoardStylersWebApp.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; }

        // For this project we will not make use of password hashing
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
