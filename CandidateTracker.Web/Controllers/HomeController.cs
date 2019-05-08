using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CandidateTracker.Web.Models;
using CandidateTracker.Data;
using Microsoft.Extensions.Configuration;

namespace CandidateTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCandidate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCandidate(Candidate c)
        {
            var repo = new CandidatesRepository(_connectionString);
            repo.AddCandidate(c);
            return RedirectToAction("Index");
        }

        public IActionResult ViewCandidates(string status)
        {
            var repo = new CandidatesRepository(_connectionString);

            if (status == null)
            {
                return Redirect("/");
            }
            var candidates = new List<Candidate>();

            if (status == "pending")
            {
                candidates = repo.GetPending();
            }
            else if (status == "confirmed")
            {
                candidates = repo.GetConfirmed();
            }
            else if (status == "declined")
            {
                candidates = repo.GetDeclined();
            }
            else
            {
                return Redirect("/");
            }
            return View(candidates);
        }

        public IActionResult ViewCandidate(int id)
        {
            var repo = new CandidatesRepository(_connectionString);
            Candidate c = repo.GetCandidate(id);
            return View(c);
        }

        public void Confirm(int id)
        {
            var repo = new CandidatesRepository(_connectionString);
            repo.Confirm(id);
        }

        public void Decline(int id)
        {
            var repo = new CandidatesRepository(_connectionString);
            repo.Decline(id);
        }

        public IActionResult GetCounts()
        {
            var repo = new CandidatesRepository(_connectionString);
            StatusCounts counts = new StatusCounts
            {
                Pending = repo.GetPendingCount(),
                Declined = repo.GetStatusCount(false),
                Confirmed = repo.GetStatusCount(true)
            };
            return Json(counts);
        }
    }
}
