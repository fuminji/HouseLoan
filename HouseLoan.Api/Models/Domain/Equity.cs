namespace HouseLoan.Api.Model.Domain
{
    public class Equity
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal OutstandingBalance { get; set; }
    }
}
