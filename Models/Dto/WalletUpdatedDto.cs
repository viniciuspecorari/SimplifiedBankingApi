namespace SimplifiedBankingApi.Models.Dto
{
    public record WalletUpdatedDto(decimal WalletBalance)
    {
        public static implicit operator WalletUpdatedDto(Wallet entity)
        {
            return new WalletUpdatedDto(entity.WalletBalance);
        }
    }
}
