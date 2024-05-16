namespace HouseLoan.Api.Model.Domain
{
    public class LoanResult
    {
        public int LoanId { get; set; }
        public LoanParam LoanParameters { get; set; }
        public List<Equity> Equities{ get; set; } 
        public List<Amortization> Amortizations { get; set; }
    }
}
