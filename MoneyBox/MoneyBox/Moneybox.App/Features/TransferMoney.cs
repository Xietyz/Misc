using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;
        private MoneyService moneyService;
        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService, MoneyService moneyService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
            this.moneyService = moneyService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            moneyService.CheckDeductionEligibility(amount, from);
            moneyService.CheckPayInEligiblity(amount, to);

            from.Balance = from.Balance - amount;
            from.Withdrawn = from.Withdrawn - amount;

            to.Balance = to.Balance + amount;
            to.PaidIn = to.PaidIn + amount;

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
