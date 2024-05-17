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
        public async Task<LoanResult> GetLoanResultAsync(int loanId)
        {
            var loanParameters = await dbContext.LoanParams.FindAsync(loanId);
            var equityInstallments = await dbContext.Equities.Where(i => i.Id == loanId).ToListAsync();
            var amortizationInstallments = await dbContext.Amortizations.Where(x => x.Id == loanId).ToListAsync();

            if (loanParameters == null)
            {
                return null; 
            }


            var loanResult = new LoanResult
            {
                LoanId = loanParameters.Id,
                LoanParameters = loanParameters,
                Equities = equityInstallments,
                Amortizations = amortizationInstallments
            };
            return loanResult;
        }
        public async Task<IEnumerable<Amortization>> GetAllLoansAsync()
        {
            return await dbContext.Amortizations
                .ToListAsync();
        }


        public async Task SaveLoanResultAsync(LoanResult loanResult)
        {
            var loanParameters = loanResult.LoanParameters;
            dbContext.LoanParams.AddAsync(loanParameters);
            await dbContext.SaveChangesAsync();

            

            foreach (var equityInstallment in loanResult.Equities)
            {
               
                dbContext.Equities.AddAsync(equityInstallment);
            }

            foreach (var amortization in loanResult.Amortizations)
            {
                
                dbContext.Amortizations.AddAsync(amortization);
            }
            await dbContext.SaveChangesAsync();
        }
   

    }
}
