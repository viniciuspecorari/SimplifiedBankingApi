using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Data;
using SimplifiedBankingApi.Models;
using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankContext _context;

        public UserRepository(BankContext context)
        {
            _context = context;
        }

        public async Task Add(UserDto userDto)
        {
            var newUser = new User
            {
                CompleteName = userDto.CompleteName,
                Document = userDto.Document,
                Email = userDto.Email, 
                Password = userDto.Password,
                Type = userDto.Type,
                CreatedAt = DateTime.Now,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
