using System;
using System.Collections.Generic;

namespace lab4.Core
{

    public class Database
    {
        private static Database _instance;

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Database();
                return _instance;
            }
        }

        private Dictionary<string, Table> tables;

        private Database()
        {
            tables = new Dictionary<string, Table>();
        }

        public void AddTable(Table table)
        {
            if (tables.ContainsKey(table.Name))
                throw new Exception($"Table '{table.Name}' already exists.");
            tables[table.Name] = table;
        }

        public Table GetTable(string name)
        {
            Table table;
            if (tables.TryGetValue(name, out table))
                return table;
            return null;
        }

        public void Clear()
        {
            tables.Clear();
        }
    }


}
