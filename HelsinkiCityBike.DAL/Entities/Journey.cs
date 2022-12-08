
namespace HelsinkiCityBike.DAL.Entities
{
    public class Journey
    {
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
        
    }
}
