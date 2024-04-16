namespace SimplifiedBankingApi.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal WalletBalance { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relações
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; } = new List<Transaction>();


    }
}
