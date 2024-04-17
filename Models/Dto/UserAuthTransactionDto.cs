namespace SimplifiedBankingApi.Models.Dto
{
    public record UserAuthTransactionDto(decimal Value,Guid payee)
    {
        public static implicit operator UserAuthTransactionDto(Transaction entity)
        {
            return new UserAuthTransactionDto(entity.Value, entity.payee);
        }
    }
}
