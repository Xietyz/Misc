using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;
        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            if (from.CheckDeductionEligibility(amount, from, notificationService))
            {
                if (to.CheckPayInEligiblity(amount, to, notificationService))
                {
                    from.Balance = from.Balance - amount;
                    from.Withdrawn = from.Withdrawn - amount;

                    to.Balance = to.Balance + amount;
                    to.PaidIn = to.PaidIn + amount;

                    this.accountRepository.Update(from);
                    this.accountRepository.Update(to);
                }
            }
        }
    }
}
