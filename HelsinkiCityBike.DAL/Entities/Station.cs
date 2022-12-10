

namespace HelsinkiCityBike.DAL.Entities
{
    public class Station
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfJourneysStartingFrom { get; set; }
        public int NumberOfJourneysEndingAt { get; set; }
    }
}
