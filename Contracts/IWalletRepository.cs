using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Contracts
{
    public interface IWalletRepository
    {
        public Task Add(Guid userId);
        public Task<WalletDto> GetWalletByUserId(Guid userId);

        public Task Deposit(Guid walletId, decimal value);
        public Task Debit(Guid walletId, decimal value);

        public Task<WalletUpdatedDto> GetBalance(Guid walletId);
    }
}
