using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Data;
using SimplifiedBankingApi.Exceptions;
using SimplifiedBankingApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using SimplifiedBankingApi.Models;

namespace SimplifiedBankingApi.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;
        private readonly IWalletRepository _walletRepository;
        static readonly HttpClient httpClient = new HttpClient();

        public TransactionRepository(BankContext bankContext, IWalletRepository walletRepository)
        {
            _context = bankContext;
            _walletRepository = walletRepository;
        }

        public async Task Transacting(UserAuthTransactionDto transaction, Guid walletId)
        {
            // Verificar se a carteira beneficiaria existe
            if (_walletRepository.GetWalletByUserId(transaction.payee) is null)
            {
                throw new Error(StatusCodes.Status401Unauthorized.ToString(), "Unauthorized transaction", "The target wallet does not exist.");
            }

            // Verificar se o valor a ser transacionado não é <= 0
            if (transaction.Value <= 0)
            {
                throw new Error(StatusCodes.Status401Unauthorized.ToString(), "Unauthorized transaction", "The amount to be traded is not allowed.");
            }

            // Verificar saldo do usuário logado
            var userAuthWalletBalance = await _walletRepository.GetBalance(walletId);
            if (userAuthWalletBalance.WalletBalance < transaction.Value)
            {
                throw new Error(StatusCodes.Status401Unauthorized.ToString(), "Unauthorized transaction", "Insufficient balance.");
            }

            // Consultar o serviço autorizador            
            Console.WriteLine(CheckAuthorization().Result); // Debug
            if (CheckAuthorization().Result)
            {
                // Debitar da carteira do pagador
                await _walletRepository.Debit(walletId, transaction.Value);

                // Depositar na carteira do beneficiario
                await _walletRepository.Deposit(transaction.payee, transaction.Value);

                // Registrar transação
                var newTransaction = new TransactionDto(transaction.Value, walletId, transaction.payee);
                await Add(newTransaction);

                // Enviar notificação aos clientes
            }

        }

        public async Task Add(TransactionDto transaction)
        {
            var newTransaction = new Transaction
            {
                Value = transaction.Value,
                CreatedAt = DateTime.Now,
                PayerWalletId = transaction.PayerWalletId,
                payee = transaction.payee

            };

            await _context.Transactions.AddAsync(newTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckAuthorization()
        {

            var response = await httpClient.GetAsync("https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);// Debug
            TransactionAuthorizer authorize = JsonSerializer.Deserialize<TransactionAuthorizer>(content);
            Console.WriteLine(authorize);// Debug

            if (authorize.message.ToLower() == "autorizado")
            {
                return true;
            }
            return false;

              
        }
    }
}
