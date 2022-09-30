using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartVotingSystem.UI.Models
{
    public class ApplicationUsers : IdentityUser
    {
        [Required]
        public string VoterId { get; set; }
        [Required]
        public string VoterName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
    }
}
