using HouseLoan.Api.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseLoan.Api.Data
{
    public class LoanDbContext: DbContext
    {
        public LoanDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
        }

        
        public DbSet<Equity> Equities { get; set; }
        public DbSet<LoanParam> LoanParams { get; set; }
        public DbSet<Amortization> Amortizations { get; set;}
    }
}
