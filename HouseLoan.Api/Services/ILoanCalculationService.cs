using HouseLoan.Api.Model.Domain;

namespace HouseLoan.Api.Services
{
    public interface ILoanCalculationService
    {
        LoanResult CalculateLoan(LoanParam loanParameteres);
    }
}
