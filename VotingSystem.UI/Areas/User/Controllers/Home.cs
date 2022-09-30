using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartVotingSystem.UI.Data;
using SmartVotingSystem.UI.Models;
using SmartVotingSystem.UI.Services;
using System.Security.Claims;

namespace SmartVotingSystem.UI.Areas.User.Controllers
{
    [Area("User")]
    public class Home : Controller
    {
        private readonly ApplicationDbContext _db;
        HttpClientHandler _clientHandler = new HttpClientHandler();

        public Home(ApplicationDbContext db)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrro) => { return true; };
            _db = db;
        }

        public IActionResult Index()
        {
            var parties = _db.PartiesMaster.ToList();
            return View(parties);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult DuplicateError()
        {
            return View();
        }

        public async Task<IActionResult> Vote(int id)
        {
            if(id == 0)
            {
                return View();
            }
            else
            {
                var party = _db.PartiesMaster.Find(id);
                if(party == null)
                {
                    return NotFound();
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    var user = await _db.ApplicationUsers.Where(n => n.Id == claim.Value).FirstOrDefaultAsync();

                    var validatevote = await (from a in _db.ApplicationUsers 
                                              join v in _db.VoteMaster on a.VoterId equals v.VoterId
                                              where a.Email == user.Email select new VoteMaster()
                                              {
                                                  VoterId = v.VoterId,
                                                  PartyVoted = v.PartyVoted,
                                                  VotedPartyType = v.VotedPartyType,
                                                  VoterName = v.VoterName,
                                              }).FirstOrDefaultAsync();

                    if (validatevote != null)
                    {
                        return RedirectToAction(nameof(DuplicateError));
                    }
                    else
                    {
                        VoteMaster vm = new VoteMaster()
                        {
                            VoterId = user.VoterId,
                            PartyVoted = party.PartyName,
                            VotedPartyType = party.PartyType,
                            VoterName = user.VoterName
                        };

                        var voteresult = new VoteMaster();
                        using (var httpclient = new HttpClient(_clientHandler))
                        {

                            using (var response = await httpclient.PostAsJsonAsync(AppConstant.BaseApiUrl + AppConstant.PostVote, vm))
                            {
                                return RedirectToAction(nameof(Success));
                                //string apiresponse = await response.Content.ReadAsStringAsync();
                                //voteresult = JsonConvert.DeserializeObject<VoteMaster>(apiresponse);
                            }
                        }
                    }

                }
            }
        }
    }
}
