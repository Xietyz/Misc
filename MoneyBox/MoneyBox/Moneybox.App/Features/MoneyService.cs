using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Features
{
    public class MoneyService
    {
        private INotificationService NotificationService;
        public MoneyService(INotificationService notificationService)
        {
            this.NotificationService = notificationService;
        }
        public void CheckPayInEligiblity(decimal amount, Account to)
        {
            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                this.NotificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
        }
        public void CheckDeductionEligibility(decimal amount, Account from)
        {
            var fromBalance = from.Balance - amount;
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                this.NotificationService.NotifyFundsLow(from.User.Email);
            }
        }
    }
}
