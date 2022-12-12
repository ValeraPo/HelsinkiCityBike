namespace HelsinkiCityBike.DAL.Entities
{
    public class Station
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfJourneysStartingFrom { get; set; }
        public int NumberOfJourneysEndingAt { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Station);
        }

        public  bool Equals(Station station)
        {
            return Name == station.Name &&
                   Address == station.Address &&
                   NumberOfJourneysStartingFrom == station.NumberOfJourneysStartingFrom &&
                   NumberOfJourneysEndingAt == station.NumberOfJourneysEndingAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Address, NumberOfJourneysStartingFrom, NumberOfJourneysEndingAt);
        }
    }
}
