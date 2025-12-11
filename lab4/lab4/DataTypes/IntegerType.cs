using lab4.DataTypes;
using System;

namespace lab4.DataTypes
{
    public class IntegerType : DataType
    {
        public override bool Validate(object value)
        {
            if (value == null) return true;
            return value is int;
        }
    }

}
