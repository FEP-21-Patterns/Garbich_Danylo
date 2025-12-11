using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4.Core
{
    public class SimpleQuery
    {
        private Table table;
        private string[] selectedColumns = null;

        private List<Tuple<string, string, object>> filters = new List<Tuple<string, string, object>>();

        public SimpleQuery(Table table)
        {
            this.table = table;
        }

        public SimpleQuery Select(string[] columns)
        {
            selectedColumns = columns;
            return this;
        }

        public SimpleQuery Where(string column, string op, object value)
        {
            filters.Add(Tuple.Create(column, op, value));
            return this;
        }

        public List<Row> Execute()
        {
            var result = table.Rows.Where(row =>
            {
                foreach (var t in filters)
                {
                    string column = t.Item1;
                    string op = t.Item2;
                    object value = t.Item3;

                    var rowValue = row[column];

                    if (op == "=" && !rowValue.Equals(value))
                        return false;

                    if (op == ">" && !(Convert.ToInt32(rowValue) > Convert.ToInt32(value)))
                        return false;

                }
                return true;
            }).ToList();

            if (selectedColumns != null)
            {
                var projected = new List<Row>();
                foreach (var row in result)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (var col in selectedColumns)
                    {
                        if (row.Data.ContainsKey(col))
                            dict[col] = row[col];
                    }
                    projected.Add(new Row(dict));
                }
                return projected;
            }

            return result;
        }
    }
}
