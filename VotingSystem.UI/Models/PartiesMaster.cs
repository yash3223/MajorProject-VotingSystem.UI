using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartVotingSystem.UI.Models
{
    [Table("PartiesMasters")]
    public class PartiesMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PartyType { get; set; }

        [Required]
        public string PartyName { get; set; }
    }
}
