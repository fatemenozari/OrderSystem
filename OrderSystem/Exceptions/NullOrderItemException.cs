namespace OrderSystem.Exceptions
{
    public class NullOrderItemException : Exception
    {
        public override string Message => " Order Item can not be null .";

    }
}
