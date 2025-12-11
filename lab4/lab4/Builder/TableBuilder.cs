using System;
using lab4.Core;
using lab4.DataTypes;
using System.Collections.Generic;

namespace lab4.Builder
{


    using System;
    using System.Collections.Generic;

    public class TableBuilder
    {
        private string tableName;
        private List<Column> columns;

        public TableBuilder(string name)
        {
            tableName = name;
            columns = new List<Column>();
        }

        /// <summary>
        /// Додає колонку до таблиці
        /// </summary>
        /// <param name="name">Назва колонки</param>
        /// <param name="type">Тип даних (IntegerType, StringType, etc.)</param>
        /// <param name="nullable">Дозволяти null</param>
        /// <param name="primaryKey">Primary key</param>
        /// <param name="foreignTable">Ім'я таблиці для FK (опціонально)</param>
        /// <param name="foreignColumn">Ім'я колонки для FK (опціонально)</param>
        /// <param name="maxLength">Максимальна довжина (для string)</param>
        /// <returns>TableBuilder для ланцюжка викликів</returns>
        public TableBuilder AddColumn(string name, DataType type, bool nullable = true, bool primaryKey = false,
                                      string foreignTable = null, string foreignColumn = null, int? maxLength = null)
        {
            Column column = new Column(name, type, nullable, primaryKey, foreignTable, foreignColumn, maxLength);
            columns.Add(column);
            return this;  
        }

        /// <summary>
        /// Створює таблицю і додає її до Database
        /// </summary>
        /// <param name="db">Database</param>
        /// <returns>Створену таблицю</returns>
        public Table Build(Database db)
        {
            if (db == null)
                throw new ArgumentNullException("Database cannot be null.");

            Table table = new Table(tableName, columns);
            db.AddTable(table);
            return table;
        }
    }

}
