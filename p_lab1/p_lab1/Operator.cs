using System;
using System.Collections.Generic;

namespace p_lab1
{
    public class Operator
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double TalkingCharge { get; set; }
        public double MessageCost { get; set; }
        public double NetworkCharge { get; set; }
        public int DiscountRate { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public Operator(int id, string name, double talkingCharge, double messageCost, double networkCharge, int discountRate)
        {
            ID = id;
            Name = name;
            TalkingCharge = talkingCharge;
            MessageCost = messageCost;
            NetworkCharge = networkCharge;
            DiscountRate = discountRate;
        }

        public void AddCustomer(Customer customer)
        {
            if (!Customers.Contains(customer))
            {
                Customers.Add(customer);
                customer.AddOperator(this);
            }
        }

        public double CalculateTalkingCost(int minute, Customer customer)
        {
            double cost = minute * TalkingCharge;
            if (customer.Age < 18 || customer.Age > 65)
                cost *= (1 - DiscountRate / 100.0);
            return cost;
        }

        public double CalculateMessageCost(int quantity, Customer customer, Customer other)
        {
            double cost = quantity * MessageCost;
            if (customer.Operators.Exists(op => other.Operators.Contains(op)))
                cost *= (1 - DiscountRate / 100.0);
            return cost;
        }

        public double CalculateNetworkCost(double amount)
        {
            return amount * NetworkCharge;
        }

        public void AcceptPayment(Customer customer, Bill bill, double amount)
        {
            if (Customers.Contains(customer) && customer.Bills.Contains(bill))
            {
                bill.Pay(amount);
                Console.WriteLine($"{Name} прийняв оплату {amount} від {customer.Name}. Новий борг: {bill.CurrentDebt}");
            }
            else
            {
                Console.WriteLine($"❌ Помилка: {customer.Name} не обслуговується у {Name} або не має цього рахунку.");
            }
        }
    }
}