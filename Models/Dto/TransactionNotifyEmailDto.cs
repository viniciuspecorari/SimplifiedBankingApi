namespace SimplifiedBankingApi.Models.Dto
{
    public record TransactionNotifyEmailDto(Guid TransactionId, string PayerName, string PayeeEmail, decimal Value, DateTime TransactionCreatedAt)
    {
        public static implicit operator TransactionNotifyEmailDto(TransactionNotifyEmail entity)
        {
            return new TransactionNotifyEmailDto(entity.TransactionId, entity.PayerName, entity.PayeeEmail, entity.Value, entity.TransactionCreatedAt);
        }
    }
}
