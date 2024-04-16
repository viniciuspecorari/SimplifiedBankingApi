using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Contracts
{
    public interface IUserRepository
    {
        public Task Add(UserDto userDto);
    }
}
