using lab4.DataTypes;
using System;

namespace lab4.Core
{
    public class Column
    {
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        public bool Nullable { get; private set; }
        public bool PrimaryKey { get; private set; }
        public Tuple<string, string> ForeignKey { get; private set; }
        public int? MaxLength { get; private set; }

        public Column(string name, DataType type, bool nullable = true, bool primaryKey = false,
                      string foreignTable = null, string foreignColumn = null, int? maxLength = null)
        {
            Name = name;
            Type = type;
            Nullable = nullable;
            PrimaryKey = primaryKey;
            MaxLength = maxLength;
            if (foreignTable != null && foreignColumn != null)
                ForeignKey = Tuple.Create(foreignTable, foreignColumn);
        }

        public bool Validate(object value)
        {
            if (value == null)
                return Nullable;
            if (!Type.Validate(value))
                return false;
            if (Type is StringType && MaxLength.HasValue && value is string s)
                return s.Length <= MaxLength.Value;
            return true;
        }
    }




}
