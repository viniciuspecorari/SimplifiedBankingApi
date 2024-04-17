using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace SimplifiedBankingApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string CompleteName { get; set; }

        [MaxLength(14)]
        public required string Document { get; set; } // Cpf ou Cnpj
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required char Type { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relações
        public ICollection<Wallet> Wallets { get; } = new List<Wallet>();
    }
}
