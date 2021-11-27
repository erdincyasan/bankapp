namespace Udemy.BankApp.Web.Models
{
    public class SendMoneyModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
    }
}
