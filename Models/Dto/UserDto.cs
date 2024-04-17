namespace SimplifiedBankingApi.Models.Dto
{
    public record UserDto(string CompleteName, string Document, string Email, string Password, char Type)
    {
        public static implicit operator UserDto(User entity)
        {
            return new UserDto(entity.CompleteName, entity.Document, entity.Email, entity.Password, entity.Type);
        }
    }
}
