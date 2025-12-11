using System;

namespace lab4.DataTypes
{

    public class DateType : DataType
    {
        public override bool Validate(object value)
        {
            if (value == null) return true;
            return value is DateTime;
        }
    }

}
