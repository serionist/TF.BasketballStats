using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TF.BasketballStats.Database
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime Timestamp { get; set; }
        [JsonIgnore]
        public byte[] ProfilePicture { get; set; }
        [JsonIgnore]
        public string ProfilePictureMime { get; set; }
        [JsonIgnore]
        public virtual ICollection<GameEvent> GameEvents { get; set; }

    }
}
