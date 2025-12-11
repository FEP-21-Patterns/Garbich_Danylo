using System;
using System.Collections.Generic;
using System.Linq;


namespace lab4.Core
{

    public class Table
    {
        public string Name { get; private set; }
        private Dictionary<string, Column> columns;
        public Dictionary<string, Column> Columns => columns;
        public List<Row> Rows { get; private set; }
        private int nextId = 1;

        public Table(string name, List<Column> columnsList)
        {
            Name = name;
            columns = columnsList.ToDictionary(c => c.Name, c => c);
            Rows = new List<Row>();

            foreach (var column in columns.Values)
            {
                if (column.ForeignKey != null)
                {
                    var fkTable = Database.Instance.GetTable(column.ForeignKey.Item1);
                    if (fkTable == null)
                        throw new Exception($"Foreign key table '{column.ForeignKey.Item1}' does not exist.");
                    if (!fkTable.Columns.ContainsKey(column.ForeignKey.Item2))
                        throw new Exception($"Foreign key column '{column.ForeignKey.Item2}' does not exist in table '{column.ForeignKey.Item1}'.");
                }
            }
        }

        public Row Insert(Dictionary<string, object> rowData)
        {
            foreach (var column in columns.Values)
            {
                if (column.PrimaryKey)
                {
                    if (!rowData.ContainsKey(column.Name))
                        throw new Exception($"Primary key column '{column.Name}' must have a value.");
                    var newValue = rowData[column.Name];
                    if (Rows.Any(r => r.Data[column.Name].Equals(newValue)))
                        throw new Exception($"Duplicate primary key value '{newValue}' in column '{column.Name}'");
                }
            }

            foreach (var column in columns.Values)
            {
                if (column.ForeignKey != null && rowData.ContainsKey(column.Name))
                {
                    var fkTable = Database.Instance.GetTable(column.ForeignKey.Item1);
                    var fkValue = rowData[column.Name];
                    if (!fkTable.Rows.Any(r => r.Data[column.ForeignKey.Item2].Equals(fkValue)))
                        throw new Exception($"Foreign key constraint failed: value '{fkValue}' not found in '{fkTable.Name}.{column.ForeignKey.Item2}'");
                }
            }

            foreach (var column in columns.Values)
            {
                object value = rowData.ContainsKey(column.Name) ? rowData[column.Name] : null;
                if (!column.Validate(value))
                    throw new Exception($"Invalid value for column '{column.Name}': {value}");
            }

            var row = new Row(rowData);
            row.Id = nextId++;
            Rows.Add(row);
            return row;
        }

        public Row FindByPrimaryKey(object pkValue)
        {
            var pkColumn = columns.Values.FirstOrDefault(c => c.PrimaryKey);
            if (pkColumn == null)
                throw new Exception("No primary key defined for this table.");
            return Rows.FirstOrDefault(r => r.Data[pkColumn.Name].Equals(pkValue));
        }
    }


}
