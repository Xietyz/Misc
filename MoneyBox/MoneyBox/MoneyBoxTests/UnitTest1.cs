using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;

namespace MoneyBoxTests
{
    public class Tests
    {

        [Test]
        public void CanTransfer()
        {
            var accRepoMock = new Mock<IAccountRepository>();
            var notificationMock = new Mock<INotificationService>();
            TransferMoney service = new TransferMoney(accRepoMock, notificationMock);
        }
    }
}