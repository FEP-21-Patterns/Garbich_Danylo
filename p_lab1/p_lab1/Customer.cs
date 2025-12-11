using System;
using System.Collections.Generic;

namespace p_lab1
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public List<Operator> Operators { get; set; } = new List<Operator>();
        public List<Bill> Bills { get; set; } = new List<Bill>();

        public Customer(int id, string name, int age)
        {
            ID = id;
            Name = name;
            Age = age;
        }

        public void AddOperator(Operator op)
        {
            if (!Operators.Contains(op))
            {
                Operators.Add(op);
                op.AddCustomer(this);
            }
        }

        public void AddBill(Bill bill)
        {
            Bills.Add(bill);
        }

        public void Talk(int minute, Customer other, Operator op, Bill bill)
        {
            double cost = op.CalculateTalkingCost(minute, this);
            if (bill.Check(cost))
                bill.Add(cost);
        }

        public void Message(int quantity, Customer other, Operator op, Bill bill)
        {
            double cost = op.CalculateMessageCost(quantity, this, other);
            if (bill.Check(cost))
                bill.Add(cost);
        }

        public void Connection(double amount, Operator op, Bill bill)
        {
            double cost = op.CalculateNetworkCost(amount);
            if (bill.Check(cost))
                bill.Add(cost);
        }

        public void PayBill(Operator op, Bill bill, double amount)
        {
            op.AcceptPayment(this, bill, amount);
        }

        public void ChangeBillLimit(Bill bill, double amount)
        {
            bill.ChangeTheLimit(amount);
        }
    }
}