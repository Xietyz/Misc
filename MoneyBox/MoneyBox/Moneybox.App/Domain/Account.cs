using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }
        public bool CheckPayInEligiblity(decimal amount, Account to, INotificationService notificationService)
        {
            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
                return false;
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
            return true;
        }
        public bool CheckDeductionEligibility(decimal amount, Account from, INotificationService notificationService)
        {
            var fromBalance = from.Balance - amount;
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
                return false;
            }

            if (fromBalance < 500m)
            {
                notificationService.NotifyFundsLow(from.User.Email);
            }
            return true;
        }
    }
}
