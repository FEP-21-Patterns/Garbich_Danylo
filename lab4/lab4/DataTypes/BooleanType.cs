namespace lab4.DataTypes
{
    public class BooleanType : DataType
    {
        public override bool Validate(object value)
        {
            if (value == null) return true;
            return value is bool;
        }
    }

}
