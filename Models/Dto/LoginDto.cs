using System.Reflection.Metadata.Ecma335;

namespace SimplifiedBankingApi.Models.Dto
{
    public record LoginDto(string Email, string Password)
    {

        public static implicit operator LoginDto(User entity)
        {
            return new LoginDto(entity.Email, entity.Password);
        }
    }
}
