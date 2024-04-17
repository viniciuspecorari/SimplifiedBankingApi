using SimplifiedBankingApi.Models.Dto;
using SimplifiedBankingApi.Models;

namespace SimplifiedBankingApi.Contracts
{
    public interface IAuthRepository
    {
        Task<Auth> GenerateToken(LoginDto login);
        Task<WalletDto> GetUserIdToken();
    }
}
