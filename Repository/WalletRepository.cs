using Microsoft.AspNetCore.Http.HttpResults;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Data;
using SimplifiedBankingApi.Exceptions;
using SimplifiedBankingApi.Models;
using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly BankContext _context;

        public WalletRepository(BankContext context)
        {
            _context = context;
        }

        public async Task Add(Guid userId)
        {
            var newWallet = new Wallet
            {
                WalletBalance = 0,
                CreatedAt = DateTime.Now,
                UserId = userId
            };

            await _context.Wallets.AddAsync(newWallet);
            await _context.SaveChangesAsync();
        }       

        public async Task<WalletDto> GetWalletByUserId(Guid userId)
        {
            var wallet = _context.Wallets.Where(x => x.UserId == userId).FirstOrDefault();
            var walletDto = new WalletDto(wallet.Id, wallet.UserId);


            return walletDto;
        }

        public async Task Deposit(Guid walletId, decimal value)
        {
            var wallet = await _context.Wallets.FindAsync(walletId);

            if (value <= 0)
            {
                throw new Error(StatusCodes.Status401Unauthorized.ToString(), "Unauthorized", "Não é possível depositar um valor negativo ou nulo");
            }

            Console.WriteLine("Depositando...");
            Console.WriteLine(walletId);
            Console.WriteLine(wallet.WalletBalance);
            Console.WriteLine(value);
            wallet.WalletBalance = wallet.WalletBalance + value;            
            Console.WriteLine(wallet.WalletBalance);
            

            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task Debit(Guid walletId, decimal value)
        {
            var wallet = await _context.Wallets.FindAsync(walletId);

            if (value <= 0)
            {
                throw new Error(StatusCodes.Status401Unauthorized.ToString(), "Unauthorized", "Não é possível depositar um valor negativo ou nulo");
            }

            Console.WriteLine("Debitando...");
            Console.WriteLine(walletId);
            Console.WriteLine(wallet.WalletBalance);
            Console.WriteLine(value);            

            Console.WriteLine(wallet.WalletBalance - value);

            wallet.WalletBalance = wallet.WalletBalance - value;

            Console.WriteLine(wallet.WalletBalance);

            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<WalletUpdatedDto> GetBalance(Guid walletId)
        {
            var wallet = await _context.Wallets.FindAsync(walletId);

            var walletBalance = new WalletUpdatedDto(wallet.WalletBalance);

            return walletBalance;
        }
    }
}
