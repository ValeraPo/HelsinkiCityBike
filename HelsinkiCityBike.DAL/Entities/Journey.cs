namespace HelsinkiCityBike.DAL.Entities
{
    public class Journey
    {
        public int Id { get; set; }
        public string DepartureStationName { get; set; }
        public string ReturnStationName { get; set; }


        private float coveredDistance;

        public float CoveredDistance 
        {
            get => coveredDistance;
            set
            {
                coveredDistance = (float)Math.Round(value / 1000, 2);
            }
        }

        private float duration;

        public float Duration 
        {
            get => duration; 
            set
            {
                duration = (float)Math.Round(value / 60, 2);
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Journey);
        }

        public  bool Equals(Journey journey)
        {
            return DepartureStationName == journey.DepartureStationName &&
                   ReturnStationName == journey.ReturnStationName &&
                   CoveredDistance == journey.CoveredDistance &&
                   Duration == journey.Duration;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DepartureStationName, ReturnStationName, coveredDistance, CoveredDistance, duration, Duration);
        }
    }
}
