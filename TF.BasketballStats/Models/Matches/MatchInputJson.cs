using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TF.BasketballStats.Models.Matches
{
    public class MatchInputJson
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsHome { get; set; }
        [Required]
        public int OpponentClubId { get; set; }
    }
}
