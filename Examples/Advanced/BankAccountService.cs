namespace Advanced
{
    public class BankAccountService
    {
        private readonly IBankAccountRepository _repository;

        public BankAccountService(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> GetBalanceAsync(int accountId)
        {
            return await _repository.GetBalanceAsync(accountId);
        }

        public async Task DepositAsync(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            await _repository.UpdateBalanceAsync(accountId, await _repository.GetBalanceAsync(accountId) + amount);
        }

        public async Task WithdrawAsync(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            var currentBalance = await _repository.GetBalanceAsync(accountId);

            if (currentBalance < amount)
                throw new InvalidOperationException("Insufficient funds.");

            await _repository.UpdateBalanceAsync(accountId, currentBalance - amount);
        }
    }

    public interface IBankAccountRepository
    {
        Task<decimal> GetBalanceAsync(int accountId);
        Task UpdateBalanceAsync(int accountId, decimal newBalance);
    }
}
