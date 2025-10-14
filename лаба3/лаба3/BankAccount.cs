using System;

namespace лаба3
{
    public class BankAccount
    {
        // Закрытые поля
        private int accountNumber;
        private decimal balance;
        private string accountType;

        // Статическая переменная для генерации уникального номера счета
        private static int nextAccountNumber = 1000;

        // Конструктор по умолчанию
        public BankAccount()
        {
            accountNumber = GenerateAccountNumber();
            balance = 0;
            accountType = "Текущий";
        }

        // Конструктор с параметрами
        public BankAccount(decimal initialBalance, string type)
        {
            accountNumber = GenerateAccountNumber();
            balance = initialBalance;
            accountType = type;
        }

        // Статический метод для генерации уникального номера счета
        private static int GenerateAccountNumber()
        {
            return nextAccountNumber++;
        }

        // Методы доступа для чтения данных
        public int GetAccountNumber()
        {
            return accountNumber;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public string GetAccountType()
        {
            return accountType;
        }

        // Методы доступа для заполнения данных
        public void SetBalance(decimal newBalance)
        {
            balance = newBalance;
        }

        public void SetAccountType(string newType)
        {
            accountType = newType;
        }

        // Метод для пополнения счета
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
            }
            else
            {
                throw new ArgumentException("Сумма пополнения должна быть положительной");
            }
        }

        // Метод для снятия со счета
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                return true;
            }
            return false;
        }

        // Метод для вывода информации о счете
        public override string ToString()
        {
            return $"Счет №: {accountNumber}\nБаланс: {balance:C}\nТип счета: {accountType}";
        }
    }
}