using System;
using lab4.Builder;
using lab4.Core;
using lab4.DataTypes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Tests
{
    public static class ProgramTests
    {
        public static void RunTests()
        {
            Console.WriteLine("=== Running Database System Tests ===");

            Database.Instance.Clear();

            TestInsert();
            TestPrimaryKey();
            TestForeignKeyValidation();
            TestSimpleQuery();
            TestJoinedTable();

            Console.WriteLine("=== All tests passed ===");
        }

        private static void TestInsert()
        {
            var users = new Table("test_users", new List<Column>
        {
            new Column("id", new IntegerType(), false, true),
            new Column("name", new StringType(50), false)
        });
            Database.Instance.AddTable(users);

            users.Insert(new Dictionary<string, object> { { "id", 1 }, { "name", "Alice" } });

            if (users.Rows.Count != 1)
                throw new Exception("TestInsert failed");
            Console.WriteLine("TestInsert passed");
        }

        private static void TestPrimaryKey()
        {
            var users = Database.Instance.GetTable("test_users");

            bool exceptionThrown = false;
            try
            {
                users.Insert(new Dictionary<string, object> { { "id", 1 }, { "name", "Alice2" } });
            }
            catch
            {
                exceptionThrown = true;
            }
            if (!exceptionThrown)
                throw new Exception("TestPrimaryKey failed");
            Console.WriteLine("TestPrimaryKey passed");
        }

        private static void TestForeignKeyValidation()
        {
            bool exceptionThrown = false;
            try
            {
                var orders = new Table("fk_orders", new List<Column>
            {
                new Column("id", new IntegerType(), false, true),
                new Column("user_id", new IntegerType(), false, false, "nonexistent", "id")
            });
                Database.Instance.AddTable(orders);
            }
            catch
            {
                exceptionThrown = true;
            }
            if (!exceptionThrown)
                throw new Exception("TestForeignKeyValidation failed");
            Console.WriteLine("TestForeignKeyValidation passed");
        }

        private static void TestSimpleQuery()
        {
            var users = Database.Instance.GetTable("test_users");
            var orders = new Table("sq_orders", new List<Column>
        {
            new Column("id", new IntegerType(), false, true),
            new Column("user_id", new IntegerType(), false, false, "test_users","id"),
            new Column("product", new StringType(50), false)
        });
            Database.Instance.AddTable(orders);

            orders.Insert(new Dictionary<string, object> { { "id", 1 }, { "user_id", 1 }, { "product", "Laptop" } });
            orders.Insert(new Dictionary<string, object> { { "id", 2 }, { "user_id", 1 }, { "product", "Phone" } });

            var query = new SimpleQuery(orders)
                .Where("user_id", "=", 1)
                .Select(new[] { "id", "product" });
            var result = query.Execute();

            if (result.Count != 2) throw new Exception("TestSimpleQuery failed");
            Console.WriteLine("TestSimpleQuery passed");
        }

        private static void TestJoinedTable()
        {
            var users = Database.Instance.GetTable("test_users");
            var orders = Database.Instance.GetTable("sq_orders");
            var join = new JoinedTable(users, orders, "id", "user_id");
            var rows = join.GetJoinedRows();
            if (rows.Count != 2) throw new Exception("TestJoinedTable failed");
            Console.WriteLine("TestJoinedTable passed");
        }
    }



}
