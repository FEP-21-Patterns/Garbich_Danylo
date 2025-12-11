using lab4.Builder;
using lab4.Core;
using lab4.DataTypes;
using lab4.Tests;
using System;
using System.Collections.Generic;

class Program
{

    static void Main()
    {
        var db = Database.Instance;

        var users = new TableBuilder("users")
            .AddColumn("id", new IntegerType(), false, true)
            .AddColumn("name", new StringType(50), false)
            .Build(db);

        var orders = new TableBuilder("orders")
            .AddColumn("id", new IntegerType(), false, true)
            .AddColumn("user_id", new IntegerType(), false, false, "users", "id")
            .AddColumn("product", new StringType(100), false)
            .Build(db);

        users.Insert(new Dictionary<string, object> { { "id", 1 }, { "name", "Alice" } });
        users.Insert(new Dictionary<string, object> { { "id", 2 }, { "name", "Bob" } });

        orders.Insert(new Dictionary<string, object> { { "id", 1 }, { "user_id", 1 }, { "product", "Laptop" } });
        orders.Insert(new Dictionary<string, object> { { "id", 2 }, { "user_id", 1 }, { "product", "Phone" } });
        orders.Insert(new Dictionary<string, object> { { "id", 3 }, { "user_id", 2 }, { "product", "Tablet" } });

        Console.WriteLine("=== Users ===");
        Print(users);

        Console.WriteLine("\n=== Orders ===");
        Print(orders);

        var q = new SimpleQuery(orders)
            .Where("user_id", "=", 1)
            .Select(new[] { "id", "product" });

        var result = q.Execute();

        Console.WriteLine("\n=== Orders of user_id = 1 ===");
        foreach (var r in result)
            Console.WriteLine($"{r["id"]} -> {r["product"]}");

        var join = new JoinedTable(users, orders, "id", "user_id");

        Console.WriteLine("\n=== JOIN users & orders ===");
        foreach (var row in join.GetJoinedRows())
            Console.WriteLine($"{row["users.name"]} bought {row["orders.product"]}");


            ProgramTests.RunTests();

    }

    static void Print(Table t)
    {
        foreach (var r in t.Rows)
        {
            Console.Write($"Row {r.Id}: ");
            foreach (var kv in r.Data)
                Console.Write($"{kv.Key}={kv.Value} ");
            Console.WriteLine();
        }
    }
}

