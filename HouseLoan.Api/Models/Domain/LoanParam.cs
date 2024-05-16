namespace HouseLoan.Api.Model.Domain
{
    public class LoanParam
    {
        public int Id { get; set; } 
        public decimal SellingPrice { get; set; } 
        public decimal ProcessingFee { get; set; } 
        public decimal ReservationFee { get; set; } 
        public DateTime ReservationDate { get; set; } 
        public int EquityTerm { get; set; } 
        public int LoanTerm { get; set; } 
        public decimal MonthlyAmortization { get; set; } 
        public decimal InterestRate { get; set; } 
        public decimal Insurance { get; set; }

        public LoanParam()
        {
            Id = Guid.NewGuid().GetHashCode();
        }
    }
    
}
