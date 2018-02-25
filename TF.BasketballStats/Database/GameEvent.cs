using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TF.BasketballStats.Database
{
    public class GameEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        [Required]
        public int Quarter { get; set; }
        public int? PlayerId { get; set; }
        public virtual Player Player { get; set; }
        [Required]
        public GameEventType Type { get; set; }
        [Required]
        public TimeSpan GameTime { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
    }
}
