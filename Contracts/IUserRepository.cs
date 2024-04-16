using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Contracts
{
    public interface IUserRepository
    {
        public Task Add(UserDto userDto);

        public Task<UserLoginDto> GetByEmail(string email);

        public Task<IEnumerable<UserDto>> Get();
    }
}
