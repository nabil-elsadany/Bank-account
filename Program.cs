using System.Security.Principal;

namespace task5
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $" {Name} {Balance}";
        }
        public static Account operator +(Account Account1, Account Account2)
        {
            Account account = new Account($" name : {Account1.Name} {Account2.Name} ,balance = {Account1.Balance + Account2.Balance}");


            return account;
        }
    }
    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }



        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }


    }
    public class SavingsAccount : Account
    {
        public double interestrata { get; set; }
        public SavingsAccount(string Name = "Unnamed Account", double Balance = 0.0, double interestrata = 0.0) : base(Name, Balance)
        {
            this.interestrata = interestrata;
        }
        public override string ToString()
        {
            return $" {Name} {Balance} {interestrata}";
        }

    }
    public class CheckingAccount : Account
    {
        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0) : base(Name, Balance)
        {

        }

        public override bool Withdraw(double amount)
        {
            double bigBalanse = Balance + 1.50;
            if (bigBalanse >= amount)
            {
                Balance -= amount + 1.50;
                return true;
            }
            return false;
        }

    }
    public class TrustAccount : SavingsAccount
    {
        public TrustAccount(string Name = "Unnamed Account", double Balance = 0.0, double interestrata = 0.0) : base(Name, Balance, interestrata)
        {

        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                Balance += amount + 50;
                return true;
            }
            else if (amount > 0 && amount < 5000)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
        int count = 1;
        public override bool Withdraw(double amount)
        {
            int year = DateTime.Now.Year;
            if (DateTime.Now.Year != year)
            {
                year = DateTime.Now.Year;
                count = 1;
            }

            double BalanceAvailable = Balance * 0.20;


            if (count <= 3 && Balance - amount >= 0 && amount <= BalanceAvailable)
            {
                Balance -= amount;
                count++;
                return true;

            }
            else if (Balance - amount < 0 || count > 3 || amount > BalanceAvailable)
            {

                return false;

            }

            return false;
        }
        public override string ToString()
        {
            return $" {Name} {Balance} {interestrata}";
        }

    }

    public class Program
    {

        static void Main()
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

           
            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Account sumAccount = trustAccounts[2] + trustAccounts[3];
            Account sumAccoun = accounts[2] + accounts[3];

           
            Console.WriteLine(sumAccount);
            Console.WriteLine(sumAccoun);
        }
    }
}

