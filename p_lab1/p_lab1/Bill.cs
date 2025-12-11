namespace p_lab1
{
    public class Bill
    {
        public double LimitingAmount { get; private set; }
        public double CurrentDebt { get; private set; }

        public Bill(double limitingAmount)
        {
            LimitingAmount = limitingAmount;
            CurrentDebt = 0;
        }

        public bool Check(double amount)
        {
            return (CurrentDebt + amount) <= LimitingAmount;
        }

        public void Add(double amount)
        {
            CurrentDebt += amount;
        }

        public void Pay(double amount)
        {
            CurrentDebt -= amount;
            if (CurrentDebt < 0) CurrentDebt = 0;
        }

        public void ChangeTheLimit(double amount)
        {
            LimitingAmount = amount;
        }
    }
}