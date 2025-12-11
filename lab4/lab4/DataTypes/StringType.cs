using System;

namespace lab4.DataTypes
{
    public class StringType : DataType
    {
        private int _maxLength;

        public StringType(int maxLength)
        {
            _maxLength = maxLength;
        }

        public override bool Validate(object value)
        {
            if (value == null) return true;
            if (!(value is string)) return false;
            return ((string)value).Length <= _maxLength;
        }
    }

}
