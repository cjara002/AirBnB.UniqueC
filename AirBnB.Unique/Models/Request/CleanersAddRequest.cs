using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnB.Unique.Models.Request
{
    public class CleanersAddRequest
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1,
            ErrorMessage = "the property {0} should have {1} maximum characters and {2} minimum characters of 1")]

        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 1,
            ErrorMessage = "the property {0} should have {1} maximum characters and {2} minimum characters of 1")]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1,
            ErrorMessage = "the property {0} should have {1} maximum characters and {2} minimum characters of 1")]
        public string City { get; set; }
        

        [Required]
        [Range (0, int.MaxValue)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int YearsInOperation { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }
    }
}
