namespace HouseLoan.Api.Model.Domain
{
    public class Amortization
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Insurance { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OutstandingBalance { get; set; }
    }
}
