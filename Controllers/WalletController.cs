using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Models.Dto;
using SimplifiedBankingApi.Repository;
using System.Runtime.InteropServices;

namespace SimplifiedBankingApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IAuthRepository _authRepository;

        public WalletController(IWalletRepository walletRepository, IAuthRepository authRepository)
        {
            _walletRepository = walletRepository;
            _authRepository = authRepository;
        }

        
        [HttpPut]
        [Route("/api/depositing")]
        public async Task<IActionResult> Deposit(WalletUpdatedDto walletUpdate)
        {
            var UserAuthWalletId = await _authRepository.GetUserIdToken(); // Recupera walletId do usuário logado
            await _walletRepository.Deposit(UserAuthWalletId.Id, walletUpdate.WalletBalance);

            return Ok();
        }

        [HttpGet]
        [Route("/api/check-balance")]
        public async Task<ActionResult<WalletUpdatedDto>> GetBalance()
        {
            var UserAuthWalletId = await _authRepository.GetUserIdToken(); // Recupera walletId do usuário logado
            var balance = await _walletRepository.GetBalance(UserAuthWalletId.Id);

            return balance;
        }
    }
}
