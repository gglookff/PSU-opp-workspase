using System;
using System.Drawing;
using System.Windows.Forms;

namespace лаба3
{
    public partial class Form1 : Form
    {
        private BankAccount currentAccount;
        private Label lblAccountNumber;
        private Label lblBalance;
        private Label lblAccountType;
        private TextBox txtAmount;
        private Button btnDeposit;
        private Button btnWithdraw;
        private Button btnCreateAccount;

        public Form1()
        {
            InitializeComponent();
            CreateNewAccount();
        }

        private void InitializeComponent()
        {
            // Настройка формы
            this.Text = "Банковский счет";
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создание элементов управления
            CreateControls();
        }

        private void CreateControls()
        {
            // Метка для номера счета
            Label accountNumberLabel = new Label()
            {
                Text = "Номер счета:",
                Location = new Point(20, 20),
                Size = new Size(80, 20)
            };

            lblAccountNumber = new Label()
            {
                Text = "",
                Location = new Point(110, 20),
                Size = new Size(100, 20)
            };

            // Метка для баланса
            Label balanceLabel = new Label()
            {
                Text = "Баланс:",
                Location = new Point(20, 50),
                Size = new Size(80, 20)
            };

            lblBalance = new Label()
            {
                Text = "",
                Location = new Point(110, 50),
                Size = new Size(100, 20)
            };

            // Метка для типа счета
            Label typeLabel = new Label()
            {
                Text = "Тип счета:",
                Location = new Point(20, 80),
                Size = new Size(80, 20)
            };

            lblAccountType = new Label()
            {
                Text = "",
                Location = new Point(110, 80),
                Size = new Size(100, 20)
            };

            // Поле для ввода суммы
            Label amountLabel = new Label()
            {
                Text = "Сумма:",
                Location = new Point(20, 110),
                Size = new Size(80, 20)
            };

            txtAmount = new TextBox()
            {
                Location = new Point(110, 110),
                Size = new Size(100, 20)
            };

            // Кнопки операций
            btnDeposit = new Button()
            {
                Text = "Пополнить",
                Location = new Point(20, 150),
                Size = new Size(80, 30)
            };

            btnWithdraw = new Button()
            {
                Text = "Снять",
                Location = new Point(110, 150),
                Size = new Size(80, 30)
            };

            btnCreateAccount = new Button()
            {
                Text = "Новый счет",
                Location = new Point(200, 150),
                Size = new Size(80, 30)
            };

            // Добавление обработчиков событий
            btnDeposit.Click += BtnDeposit_Click;
            btnWithdraw.Click += BtnWithdraw_Click;
            btnCreateAccount.Click += BtnCreateAccount_Click;

            // Добавление элементов на форму
            this.Controls.Add(accountNumberLabel);
            this.Controls.Add(lblAccountNumber);
            this.Controls.Add(balanceLabel);
            this.Controls.Add(lblBalance);
            this.Controls.Add(typeLabel);
            this.Controls.Add(lblAccountType);
            this.Controls.Add(amountLabel);
            this.Controls.Add(txtAmount);
            this.Controls.Add(btnDeposit);
            this.Controls.Add(btnWithdraw);
            this.Controls.Add(btnCreateAccount);
        }

        private void CreateNewAccount()
        {
            currentAccount = new BankAccount(1000, "Текущий");
            UpdateAccountInfo();
        }

        private void UpdateAccountInfo()
        {
            lblAccountNumber.Text = currentAccount.GetAccountNumber().ToString();
            lblBalance.Text = currentAccount.GetBalance().ToString("C");
            lblAccountType.Text = currentAccount.GetAccountType();
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                currentAccount.Deposit(amount);
                UpdateAccountInfo();
                txtAmount.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректную сумму");
            }
        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                if (currentAccount.Withdraw(amount))
                {
                    UpdateAccountInfo();
                    txtAmount.Clear();
                }
                else
                {
                    MessageBox.Show("Недостаточно средств");
                }
            }
            else
            {
                MessageBox.Show("Введите корректную сумму");
            }
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            CreateNewAccount();
        }
    }
}