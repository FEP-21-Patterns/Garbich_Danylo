using System.Collections.Generic;

namespace lab4.Core
{

    public class JoinedTable
    {
        private Table table1;
        private Table table2;
        private string key1;
        private string key2;

        public JoinedTable(Table t1, Table t2, string key1, string key2)
        {
            table1 = t1;
            table2 = t2;
            this.key1 = key1;
            this.key2 = key2;
        }

        public List<Dictionary<string, object>> GetJoinedRows()
        {
            var result = new List<Dictionary<string, object>>();

            foreach (var row1 in table1.Rows)
            {
                foreach (var row2 in table2.Rows)
                {
                    if (row1[key1].Equals(row2[key2]))
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (var c in table1.Columns.Keys)
                            dict[$"{table1.Name}.{c}"] = row1[c];
                        foreach (var c in table2.Columns.Keys)
                            dict[$"{table2.Name}.{c}"] = row2[c];
                        result.Add(dict);
                    }
                }
            }

            return result;
        }
    }


}
