namespace SmartVotingSystem.UI.Services
{
    public static class AppConstant
    {
        public const string Admin = "Admin";
        public const string User = "User";


        //api constants
        public const string BaseApiUrl = "https://localhost:7071";

        public const string GetPartyList = "/api/VotingService/getparties";
        public const string AddParty = "/api/VotingService/addparty";
        public const string EditParty = "/api/VotingService/editparty";
        public const string DeleteParty = "/api/VotingService/deleteparty";


        public const string GetVoteList = "/api/VotingService/getvotes";

        public const string PostVote = "/api/VotingService/addvote";
    }
}
