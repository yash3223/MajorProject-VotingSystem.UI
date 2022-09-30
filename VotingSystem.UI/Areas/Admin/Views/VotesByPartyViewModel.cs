using SmartVotingSystem.UI.Models;

namespace VotingSystem.UI.Areas.Admin.Views
{
    public class VotesByPartyViewModel
    {
        public PartiesMaster partiesMaster { get; set; }

        public List<VoteMaster> votes { get; set; }
    }
}
