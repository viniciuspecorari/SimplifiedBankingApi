using System.Reflection.Metadata.Ecma335;

namespace SimplifiedBankingApi.Models.Dto
{
    public record UserLoginDto(Guid Id, string Email, string Password, char Type)
    {

        public static implicit operator UserLoginDto(User entity)
        {
            return new UserLoginDto(entity.Id, entity.Email, entity.Password, entity.Type);
        }
    }
}
