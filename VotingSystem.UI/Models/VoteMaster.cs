using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartVotingSystem.UI.Models
{
    [Table("VoteMasters")]
    public class VoteMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VoterId { get; set; }

        [Required]
        public string VoterName { get; set; }

        [Required]
        public string PartyVoted { get; set; }

        [Required]
        public string VotedPartyType { get; set; }
    }
}
