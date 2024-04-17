using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Models.Dto;
using SimplifiedBankingApi.Repository;

namespace SimplifiedBankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthRepository _authRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAuthRepository authRepository)
        {
            _transactionRepository = transactionRepository;
            _authRepository = authRepository;
        }

        [Authorize(Roles = "C")] // Somente usuários comuns = C podem transferir
        [HttpPost]
        [Route("/api/transfer")]
        public async Task<IActionResult> Transacting(UserAuthTransactionDto transaction)
        {
            var UserAuthWalletId = await _authRepository.GetUserIdToken(); // Recupera walletId do usuário logado

            await _transactionRepository.Transacting(transaction, UserAuthWalletId.Id);

            return Ok();
        }
    }
}
