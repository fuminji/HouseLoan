using HouseLoan.Api.Model.Domain;

namespace HouseLoan.Api.Services
{
    public class LoanCalculationService : ILoanCalculationService
    {
        public LoanResult CalculateLoan(LoanParam loanParameters)
        {
            var result = new LoanResult
            {
                Equities = new List<Equity>(),
                Amortizations = new List<Amortization>()
            };

            // Calculate the Total Package Price
            var totalPackagePrice = loanParameters.SellingPrice + loanParameters.ProcessingFee;
            // Calculate the Equity
            var equity = totalPackagePrice * 0.125m; // 12.5% of Total Package Price

            //Calculate the Equity Scheme (amount to be paid each month for the Equity Term)
            var equityScheme = (equity - loanParameters.ReservationFee) / loanParameters.EquityTerm;

            //Calculate the Loanable Amout
            var loanableAmount = totalPackagePrice - equity;

            var firstEquityDueDate = loanParameters.ReservationDate.AddMonths(1);

            //Calculate Equity installments
            for (int i = 0; i < loanParameters.EquityTerm; i++)
                {
                var dueDate = firstEquityDueDate.AddMonths(i);
                var outstandingBalance = totalPackagePrice - loanParameters.ReservationFee - (equityScheme * (i + 1));

                result.Equities.Add(new Equity
                {
                    DueDate = dueDate,
                    Amount = equityScheme,
                    OutstandingBalance = outstandingBalance,
                });
            }

            // Calculate the first due date for Amortization installments
            var firstAmortizationDueDate = firstEquityDueDate.AddMonths(loanParameters.EquityTerm);

            decimal outstandingBalanceForAmortization = loanableAmount;
            for (int i = 0; i < loanParameters.LoanTerm; i++)
            {
                var dueDate = firstAmortizationDueDate.AddMonths(i);
                var interest = outstandingBalanceForAmortization * (loanParameters.InterestRate / 12 / 100);
                var principal = loanParameters.MonthlyAmortization - loanParameters.Insurance - interest;
                outstandingBalanceForAmortization -= principal;

                result.Amortizations.Add(new Amortization
                {
                    DueDate = dueDate,
                    Insurance = loanParameters.Insurance,
                    Interest = interest,
                    Principal = principal,
                    TotalAmount = loanParameters.MonthlyAmortization,
                    OutstandingBalance = outstandingBalanceForAmortization

                });
            }
            return result;
        }
    }
}
