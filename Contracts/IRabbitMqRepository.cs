using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Contracts
{
    public interface IRabbitMqRepository
    {
        void SendingEmail(TransactionNotifyEmailDto notifyEmail);
    }
}
