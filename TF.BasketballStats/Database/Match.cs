using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TF.BasketballStats.Database
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsHome { get; set; }
        [Required]
        public int OpponentClubId { get; set; }
        [ForeignKey("OpponentClubId")]
        public virtual Club OpponentClub { get; set; }
        public DateTime Timestamp { get; set; }
        [JsonIgnore]
        public virtual ICollection<GameEvent> GameEvents { get; set; }
    }
}
