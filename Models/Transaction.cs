namespace SimplifiedBankingApi.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relações
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }

    }
}
