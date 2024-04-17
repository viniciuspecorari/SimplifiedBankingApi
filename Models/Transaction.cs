namespace SimplifiedBankingApi.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relações
        public Guid PayerWalletId { get; set; }      
        public Guid payee { get; set; }
        public Wallet Wallet { get; set; }

    }
}
