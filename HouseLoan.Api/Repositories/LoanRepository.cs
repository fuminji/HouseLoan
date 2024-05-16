using HouseLoan.Api.Data;
using HouseLoan.Api.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseLoan.Api.Repositories
{
    public class LoanRepository : ILoanRepository 
    {
        private readonly LoanDbContext dbContext;

        public LoanRepository(LoanDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<LoanResult> GetLoanResultAsync(int Id)
        {
            var loanParameters = await dbContext.LoanParams.FindAsync(Id);
            var equityInstallments = await dbContext.Equities.Where(i => i.Id == Id).ToListAsync();
            var amortizationInstallments = await dbContext.Amortizations.Where(x => x.Id == Id).ToListAsync();

            var loanResult = new LoanResult
            {
                LoanId = Id,
                LoanParameters = loanParameters,
                Equities = equityInstallments,
                Amortizations = amortizationInstallments
            };
            return loanResult;
        }

        public async Task SaveLoanResultAsync(LoanResult loanResult)
        {
            var loanParameters = loanResult.LoanParameters;
            dbContext.LoanParams.AddAsync(loanParameters);
            await dbContext.SaveChangesAsync();

            var loanId = loanParameters.Id;

            foreach (var equityInstallment in loanResult.Equities)
            {
                equityInstallment.Id = loanId;
                dbContext.Equities.AddAsync(equityInstallment);
            }

            foreach (var amortization in loanResult.Amortizations)
            {
                amortization.Id = loanId;
                dbContext.Amortizations.AddAsync(amortization);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
