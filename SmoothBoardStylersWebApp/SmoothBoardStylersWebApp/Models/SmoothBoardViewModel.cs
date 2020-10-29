using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmoothBoardStylersWebApp.Models
{
    public class SmoothBoardViewModel
    {
        [Required]
        public string Model { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile SmoothBoardImage { get; set; }
    }
}
