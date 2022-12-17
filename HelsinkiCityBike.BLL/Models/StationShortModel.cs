namespace HelsinkiCityBike.BLL.Models
{
    public class StationShortModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as StationShortModel);
        }

        public bool Equals(StationShortModel station)
        {
            return Name == station.Name &&
                   Address == station.Address;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Address);
        }
    }
}
