namespace SimplifiedBankingApi.Models.Dto
{
    public record TransactionDto(decimal Value, Guid PayerWalletId, Guid payee)
    {
        public static implicit operator TransactionDto(Transaction entity)
        {
            return new TransactionDto(entity.Value, entity.PayerWalletId, entity.payee);
        }
    }
}
