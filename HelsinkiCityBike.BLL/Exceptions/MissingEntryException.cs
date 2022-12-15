namespace HelsinkiCityBike.BLL.Exceptions
{
    public class MissingEntryException : ArgumentOutOfRangeException
    {
        public MissingEntryException(string message) : base(message)
        { }
    }
}
