using Microsoft.EntityFrameworkCore;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Data;
using SimplifiedBankingApi.Exceptions;
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

        public async Task<UserLoginDto> GetByEmail(string email)
        {
            var user = _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();

            var login = new UserLoginDto(user.Id, user.Email, user.Password, user.Type);

            return login;
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await _context.Users
            .Select(x => new UserDto(x.CompleteName, x.Document, x.Email, x.Password, x.Type)).ToListAsync();            

            return users;
        }
    }
}
