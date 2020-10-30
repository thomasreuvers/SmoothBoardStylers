using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmoothBoardStylersWebApp.Models
{
    public class BoardInformationViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ModelName { get; set; }
    }
}
