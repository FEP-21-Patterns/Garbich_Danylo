using System.Collections.Generic;

namespace lab4.Core
{


    public class Row
    {
        public int Id { get; set; }
        public Dictionary<string, object> Data { get; private set; }

        public Row(Dictionary<string, object> data)
        {
            Data = new Dictionary<string, object>(data);
        }

        public object this[string columnName]
        {
            get => Data.ContainsKey(columnName) ? Data[columnName] : null;
            set => Data[columnName] = value;
        }
    }



}
