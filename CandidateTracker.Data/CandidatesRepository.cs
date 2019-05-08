using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandidateTracker.Data
{
    public class CandidatesRepository
    {
        private string _connectionString;

        public CandidatesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCandidate(Candidate c)
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                ctx.Candidates.Add(c);
                ctx.SaveChanges();
            }
        }

        public List<Candidate> GetPending()
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Confirmed == null).ToList();
            }
        }

        public List<Candidate> GetConfirmed()
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Confirmed.Value).ToList();
            }
        }

        public List<Candidate> GetDeclined()
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                return ctx.Candidates.Where(c => !c.Confirmed.Value).ToList();
            }
        }

        public Candidate GetCandidate(int id)
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                return ctx.Candidates.FirstOrDefault(c => c.Id == id);
            }
        }

        public void Confirm(int id)
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                Candidate c = ctx.Candidates.FirstOrDefault(ca => ca.Id == id);
                c.Confirmed = true;
                ctx.SaveChanges();
            }
        }

        public void Decline(int id)
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                Candidate c = ctx.Candidates.FirstOrDefault(ca => ca.Id == id);
                c.Confirmed = false;
                ctx.SaveChanges();
            }
        }

        public int GetStatusCount(bool status)
        { 
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Confirmed == status).Count();
            }
        }
        public int GetPendingCount()
        {
            using (CandidatesContext ctx = new CandidatesContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Confirmed == null).Count();
            }
        }
    }
}
