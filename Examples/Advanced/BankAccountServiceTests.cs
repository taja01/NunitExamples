using Moq;

namespace Advanced
{
    [TestFixture]
    public class BankAccountServiceTests
    {
        private Mock<IBankAccountRepository> _repositoryMock;
        private BankAccountService _service;

        [SetUp]
        public void Setup()
        {
            // Arrange: Create a mock repository and initialize the service.
            _repositoryMock = new Mock<IBankAccountRepository>();
            _service = new BankAccountService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetBalanceAsync_ShouldReturnCorrectBalance()
        {
            // Arrange
            const int accountId = 1;
            const decimal expectedBalance = 1000m;

            _repositoryMock.Setup(repo => repo.GetBalanceAsync(accountId))
                .ReturnsAsync(expectedBalance);

            // Act
            var balance = await _service.GetBalanceAsync(accountId);

            // Assert
            Assert.That(balance, Is.EqualTo(expectedBalance));
            _repositoryMock.Verify(repo => repo.GetBalanceAsync(accountId), Times.Once);
        }

        [Test]
        public async Task DepositAsync_ShouldUpdateBalanceCorrectly()
        {
            // Arrange
            const int accountId = 1;
            const decimal initialBalance = 1000m;
            const decimal depositAmount = 500m;
            const decimal updatedBalance = 1500m;

            _repositoryMock.Setup(repo => repo.GetBalanceAsync(accountId))
                .ReturnsAsync(initialBalance);

            _repositoryMock.Setup(repo => repo.UpdateBalanceAsync(accountId, updatedBalance))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DepositAsync(accountId, depositAmount);

            // Assert
            _repositoryMock.Verify(repo => repo.UpdateBalanceAsync(accountId, updatedBalance), Times.Once);
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

            _repositoryMock.Setup(repo => repo.GetBalanceAsync(accountId))
                .ReturnsAsync(initialBalance);

            _repositoryMock.Setup(repo => repo.UpdateBalanceAsync(accountId, updatedBalance))
                .Returns(Task.CompletedTask);

            // Act
            await _service.WithdrawAsync(accountId, withdrawalAmount);

            // Assert
            _repositoryMock.Verify(repo => repo.UpdateBalanceAsync(accountId, updatedBalance), Times.Once);
        }

        [Test]
        public void WithdrawAsync_ShouldThrowException_WhenInsufficientFunds()
        {
            // Arrange
            const int accountId = 1;
            const decimal initialBalance = 100m;
            const decimal withdrawalAmount = 200m;

            _repositoryMock.Setup(repo => repo.GetBalanceAsync(accountId))
                .ReturnsAsync(initialBalance);

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

            _repositoryMock.Setup(repo => repo.GetBalanceAsync(accountId))
                .ReturnsAsync(initialBalance);

            if (expectedBalance >= 0)
            {
                _repositoryMock.Setup(repo => repo.UpdateBalanceAsync(accountId, expectedBalance))
                    .Returns(Task.CompletedTask);

                // Act
                await _service.WithdrawAsync(accountId, withdrawalAmount);

                // Assert
                _repositoryMock.Verify(repo => repo.UpdateBalanceAsync(accountId, expectedBalance), Times.Once);
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
