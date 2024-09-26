using Bank.WebApi.Models;
using NUnit.Framework;

namespace Bank.WebApi.Tests
{
    public class BankAccountTests
    {
        [Test]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [Test]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 10.00;
            double creditAmount = 5.00;
            double expected = 15.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }

        [Test]
        public void Credit_WithNegativeAmount_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double beginningBalance = 11.99;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(-5.00));
            Assert.AreEqual("amount", ex.ParamName);
        }

        [Test]
        public void Debit_WithAmountGreaterThanBalance_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            double beginningBalance = 10.00;
            double debitAmount = 15.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
            Assert.AreEqual("amount", ex.ParamName);
        }

        [Test]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            string customerName = "Mr. Bryan Walton";
            double beginningBalance = 100.00;

            // Act
            BankAccount account = new BankAccount(customerName, beginningBalance);

            // Assert
            Assert.AreEqual(customerName, account.CustomerName);
            Assert.AreEqual(beginningBalance, account.Balance);
        }

        [Test]
        public void Debit_WithAmountEqualToBalance_UpdatesBalanceToZero()
        {
            // Arrange
            double beginningBalance = 10.00;
            double debitAmount = 10.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(0.00, actual, 0.001, "Account not debited correctly to zero");
        }

        [Test]
        public void MultipleTransactions_UpdatesBalanceCorrectly()
        {
            // Arrange
            double beginningBalance = 100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Credit(50.00);  // Balance should now be 150.00
            account.Debit(20.00);   // Balance should now be 130.00
            account.Credit(30.00);  // Balance should now be 160.00
            account.Debit(160.00);  // Balance should now be 0.00

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(0.00, actual, 0.001, "Account balance not updated correctly after multiple transactions");
        }

    }
}