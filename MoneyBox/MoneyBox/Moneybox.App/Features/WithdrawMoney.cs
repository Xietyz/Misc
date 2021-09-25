using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;
        private MoneyService moneyService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService, MoneyService moneyService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
            this.moneyService = moneyService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
            var from = this.accountRepository.GetAccountById(fromAccountId);

            moneyService.CheckDeductionEligibility(amount, from);

            from.Balance = from.Balance - amount;
            from.Withdrawn = from.Withdrawn - amount;

            this.accountRepository.Update(from);
        }
    }
}
