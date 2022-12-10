namespace HelsinkiCityBike.BLL.Exceptions
{
    public class MissingEntryException : Exception
    {
        public MissingEntryException(string message) : base(message)
        { }
    }
}
