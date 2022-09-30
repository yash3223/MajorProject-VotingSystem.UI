using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartVotingSystem.UI.Data;
using SmartVotingSystem.UI.Models;
using SmartVotingSystem.UI.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.UI.Areas.Admin.Views;

namespace SmartVotingSystem.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Manage : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        private readonly ApplicationDbContext _db;

        public Manage(ApplicationDbContext db)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrro) => { return true; };
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var url = string.Format(AppConstant.GetPartyList);
                var result = new List<PartiesMaster>();

                using (var httpclient = new HttpClient(_clientHandler))
                {
                    using (var responseStream = await httpclient.GetAsync(AppConstant.BaseApiUrl + url))
                    {
                        string Response = await responseStream.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<PartiesMaster>>(Response);
                    }
                }

                var totalvoters = _db.VoteMaster.Count().ToString();
                var totalparties = _db.PartiesMaster.Count().ToString();
                var totalRegistered = _db.ApplicationUsers.Count().ToString();

                ViewData["voters"] = totalvoters;
                ViewData["parties"] = totalparties;
                ViewData["users"] = totalRegistered;

                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddElectionParty()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddElectionParties(PartiesMaster pm)
        {
            try
            {

                var partyresult = new PartiesMaster();
                using (var httpclient = new HttpClient(_clientHandler))
                {

                    using (var response = await httpclient.PostAsJsonAsync(AppConstant.BaseApiUrl + AppConstant.AddParty, pm))
                    {
                        string apiresponse = await response.Content.ReadAsStringAsync();
                        partyresult = JsonConvert.DeserializeObject<PartiesMaster>(apiresponse);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateElectionParties(int id)
        {
            var url = string.Format(AppConstant.GetPartyList);
                var result = new List<PartiesMaster>();

                using (var httpclient = new HttpClient(_clientHandler))
                {
                    using (var responseStream = await httpclient.GetAsync(AppConstant.BaseApiUrl + url))
                    {
                        string Response = await responseStream.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<PartiesMaster>>(Response);
                    }
                }

            var partybyid = result.Where(x => x.Id == id).FirstOrDefault();

            if(partybyid == null)
            {
                return View();
            }
            else
            {
                return View(partybyid);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateElectionParty(PartiesMaster pm)
        {
            try
            {
                var partyresult = new PartiesMaster();
                using (var httpclient = new HttpClient(_clientHandler))
                {

                    using (var response = await httpclient.PutAsJsonAsync(AppConstant.BaseApiUrl + AppConstant.EditParty, pm))
                    {
                        string apiresponse = await response.Content.ReadAsStringAsync();
                        partyresult = JsonConvert.DeserializeObject<PartiesMaster>(apiresponse);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<IActionResult> DeleteParty(PartiesMaster pm)
        {
            try
            {
                //var partyresult = new PartiesMaster();
                //using (var httpclient = new HttpClient(_clientHandler))
                //{

                //    using (var response = await httpclient.PostAsJsonAsync(AppConstant.BaseApiUrl + AppConstant.DeleteParty, pm))
                //    {
                //        string apiresponse = await response.Content.ReadAsStringAsync();
                //        partyresult = JsonConvert.DeserializeObject<PartiesMaster>(apiresponse);
                //    }
                //}

                if (pm.Id != null)
                {
                    _db.PartiesMaster.Remove(pm);
                    await _db.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<IActionResult> ViewVotes(int id)
        {
            var url = string.Format(AppConstant.GetVoteList);
            var result = new List<PartiesMaster>();

            using (var httpclient = new HttpClient(_clientHandler))
            {
                using (var responseStream = await httpclient.GetAsync(AppConstant.BaseApiUrl + url))
                {
                    string Response = await responseStream.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<PartiesMaster>>(Response);
                }
            }

            var PartybyId = await(from p in _db.PartiesMaster where p.Id == id select p).FirstOrDefaultAsync();

            var VotesbyParty = await (from v in _db.VoteMaster where v.VotedPartyType == PartybyId.PartyType && v.PartyVoted == PartybyId.PartyName select v).ToListAsync();

            if (PartybyId == null)
            {
                return View();
            }
            else
            {
                VotesByPartyViewModel vm = new VotesByPartyViewModel()
                {
                    votes = VotesbyParty,
                    partiesMaster = PartybyId
                };

                return View(vm);
            }
        }
    }
}
