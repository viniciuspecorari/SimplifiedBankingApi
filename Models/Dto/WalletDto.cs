namespace SimplifiedBankingApi.Models.Dto
{
    public record WalletDto(Guid Id, Guid UserId)
    {
        public static implicit operator WalletDto(Wallet entity)
        {
            return new WalletDto(entity.Id, entity.UserId);
        }
    }
}
