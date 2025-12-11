using System;

namespace p_lab1
{
    class Program
    {
        static void Main()
        {
            Operator op1 = new Operator(1, "Kyivstar", 1.5, 0.2, 0.05, 20);
            Operator op2 = new Operator(2, "Vodafone", 2.0, 0.25, 0.04, 15);

            Customer c1 = new Customer(1, "Alice", 17);
            Customer c2 = new Customer(2, "Bob", 70);

            Bill b1 = new Bill(100.0);
            Bill b2 = new Bill(150.0);

            c1.AddBill(b1);
            c2.AddBill(b2);

            c1.AddOperator(op1);
            c1.AddOperator(op2);
            c2.AddOperator(op2);

            c1.Talk(10, c2, op1, b1);
            c1.Message(5, c2, op2, b1);
            c2.Connection(200, op2, b2);

            c1.PayBill(op1, b1, 20);
            c2.PayBill(op2, b2, 50);

            Console.WriteLine($"{c1.Name} має борг: {b1.CurrentDebt} / {b1.LimitingAmount}");
            Console.WriteLine($"{c2.Name} має борг: {b2.CurrentDebt} / {b2.LimitingAmount}");

            Console.ReadKey();
        }
    }
}