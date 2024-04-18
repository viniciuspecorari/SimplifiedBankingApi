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
using Microsoft.EntityFrameworkCore;

namespace SimplifiedBankingApi.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;
        private readonly IWalletRepository _walletRepository;
        private readonly IRabbitMqRepository _rabbitMqRepository;
        static readonly HttpClient httpClient = new HttpClient();

        public TransactionRepository(BankContext bankContext, IWalletRepository walletRepository, IRabbitMqRepository rabbitMqRepository)
        {
            _context = bankContext;
            _walletRepository = walletRepository;
            _rabbitMqRepository = rabbitMqRepository;
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
                var transactionId = await Add(newTransaction);

                // Enviar notificação aos clientes
                var transactionNotifyEmail = await PayeeNotify(transactionId);
                _rabbitMqRepository.SendingEmail(transactionNotifyEmail);

            }

        }

        public async Task<Guid> Add(TransactionDto transaction)
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


            return newTransaction.Id;
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

        public async Task<TransactionNotifyEmailDto> PayeeNotify(Guid transactionId)
        {
            var notify = from transaction in _context.Transactions
                         where transaction.Id == transactionId
                         join wallet in _context.Wallets on transaction.PayerWalletId equals wallet.Id
                         join wallet2 in _context.Wallets on transaction.payee equals wallet2.Id
                         join user in _context.Users on wallet.UserId equals user.Id
                         join user2 in _context.Users on wallet2.UserId equals user2.Id
                         select new TransactionNotifyEmailDto(

                               transaction.Id
                             , user.CompleteName // Nome do pagador
                             , user2.Email // E-mail do beneficiario
                             , transaction.Value
                             , transaction.CreatedAt
                             );

            return notify.ToList().FirstOrDefault();
        }
    }
}
