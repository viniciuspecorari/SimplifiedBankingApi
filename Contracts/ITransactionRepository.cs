using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Contracts
{
    public interface ITransactionRepository
    {
        public Task Transacting(UserAuthTransactionDto transaction, Guid userAuth);
    }
}
