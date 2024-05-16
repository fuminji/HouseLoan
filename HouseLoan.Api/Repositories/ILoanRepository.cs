using HouseLoan.Api.Model.Domain;

namespace HouseLoan.Api.Repositories
{
    public interface ILoanRepository
    {
        Task<LoanResult> GetLoanResultAsync(int Id);
        Task SaveLoanResultAsync(LoanResult loanResult);
    }
}
