using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTracker.Data
{
    public class CandidatesContext : DbContext
    {
        private string _connectionString;

        public DbSet<Candidate> Candidates { get; set; }

        public CandidatesContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
