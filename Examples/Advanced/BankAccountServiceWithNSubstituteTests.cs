using NSubstitute;

namespace Advanced
{
    [TestFixture]
    internal class BankAccountServiceWithNSubstituteTests
    {
        private IBankAccountRepository _repositoryMock;
        private BankAccountService _service;

        [SetUp]
        public void Setup()
        {
            // Arrange: Create a substitute repository and initialize the service.
            _repositoryMock = Substitute.For<IBankAccountRepository>();
            _service = new BankAccountService(_repositoryMock);
        }

        [Test]
        public async Task GetBalanceAsync_ShouldReturnCorrectBalance()
        {
            // Arrange
            const int accountId = 1;
            const decimal expectedBalance = 1000m;

            _repositoryMock.GetBalanceAsync(accountId).Returns(expectedBalance);

            // Act
            var balance = await _service.GetBalanceAsync(accountId);

            // Assert
            Assert.That(balance, Is.EqualTo(expectedBalance));
            await _repositoryMock.Received(1).GetBalanceAsync(accountId);
        }

        [Test]
        public async Task DepositAsync_ShouldUpdateBalanceCorrectly()
        {
            // Arrange
            const int accountId = 1;
            const decimal initialBalance = 1000m;
            const decimal depositAmount = 500m;
            const decimal updatedBalance = 1500m;

            _repositoryMock.GetBalanceAsync(accountId).Returns(initialBalance);

            // Act
            await _service.DepositAsync(accountId, depositAmount);

            // Assert
            await _repositoryMock.Received(1).UpdateBalanceAsync(accountId, updatedBalance);
        }

        [Test]
        public void DepositAsync_ShouldThrowException_WhenAmountIsZeroOrNegative()
        {
            // Arrange
            const int accountId = 1;
            const decimal invalidDepositAmount = -100m;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _service.DepositAsync(accountId, invalidDepositAmount));
        }

        [Test]
        public async Task WithdrawAsync_ShouldUpdateBalanceCorrectly_WhenSufficientFunds()
        {
            // Arrange
            const int accountId = 1;
            const decimal initialBalance = 1000m;
            const decimal withdrawalAmount = 500m;
            const decimal updatedBalance = 500m;

            _repositoryMock.GetBalanceAsync(accountId).Returns(initialBalance);

            // Act
            await _service.WithdrawAsync(accountId, withdrawalAmount);

            // Assert
            await _repositoryMock.Received(1).UpdateBalanceAsync(accountId, updatedBalance);
        }

        [Test]
        public void WithdrawAsync_ShouldThrowException_WhenInsufficientFunds()
        {
            // Arrange
            const int accountId = 1;
            const decimal initialBalance = 100m;
            const decimal withdrawalAmount = 200m;

            _repositoryMock.GetBalanceAsync(accountId).Returns(initialBalance);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.WithdrawAsync(accountId, withdrawalAmount));
        }

        [Test]
        [TestCase(500, 1000, 500)]  // Success case
        [TestCase(1500, 1000, -500)] // Failure case: Insufficient funds
        public async Task WithdrawAsync_ShouldBehaveCorrectly_WithTestCases(decimal withdrawalAmount, decimal initialBalance, decimal expectedBalance)
        {
            const int accountId = 1;

            _repositoryMock.GetBalanceAsync(accountId).Returns(initialBalance);

            if (expectedBalance >= 0)
            {
                // Act
                await _service.WithdrawAsync(accountId, withdrawalAmount);

                // Assert
                await _repositoryMock.Received(1).UpdateBalanceAsync(accountId, expectedBalance);
            }
            else
            {
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _service.WithdrawAsync(accountId, withdrawalAmount));
            }
        }
    }
}
