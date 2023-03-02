namespace Cdb.Domain.Models
{
    public class CdbResponse
    {
        public CdbResponse(double grossTotalAmount, double amountInvested, double interestAmount, double netTotalAmount)
        {
            GrossTotalAmount = grossTotalAmount;
            AmountInvested = amountInvested;
            InterestAmount = interestAmount;
            NetTotalAmount = netTotalAmount;
        }

        public double GrossTotalAmount { get; private set; }
        public double AmountInvested { get; private set; }
        public double InterestAmount { get; private set; }
        public double NetTotalAmount { get; private set; }
    }
}
