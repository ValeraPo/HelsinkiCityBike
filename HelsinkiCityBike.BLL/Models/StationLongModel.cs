﻿using HelsinkiCityBike.DAL.Entities;

namespace HelsinkiCityBike.BLL.Models
{
    public class StationLongModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfJourneysStartingFrom { get; set; }
        public int NumberOfJourneysEndingAt { get; set; }
        public List<StationShortModel> TopDepartureStations { get; set; }
        public List<StationShortModel> TopReturnStations { get; set; }
        
        private float avgDistanceOfJourneyStartingFrom;

        public float AvgDistanceOfJourneyStartingFrom
        {
            get => avgDistanceOfJourneyStartingFrom;
            set
            {
                avgDistanceOfJourneyStartingFrom = (float)Math.Round(value / 1000 / NumberOfJourneysStartingFrom, 2);
            }
        }

        private float avgDistanceOfJourneyEndingAt;

        public float AvgDistanceOfJourneyEndingAt
    {
            get => avgDistanceOfJourneyEndingAt;
            set
            {
            avgDistanceOfJourneyEndingAt = (float)Math.Round(value / 1000 / NumberOfJourneysEndingAt, 2);
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as StationLongModel);
        }

        public bool Equals(StationLongModel station)
        {
            return Name == station.Name &&
                   Address == station.Address &&
                   NumberOfJourneysStartingFrom == station.NumberOfJourneysStartingFrom &&
                   NumberOfJourneysEndingAt == station.NumberOfJourneysEndingAt &&
                   AvgDistanceOfJourneyStartingFrom == station.AvgDistanceOfJourneyStartingFrom &&
                   AvgDistanceOfJourneyEndingAt == station.AvgDistanceOfJourneyEndingAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Address, NumberOfJourneysStartingFrom, NumberOfJourneysEndingAt);
        }
    }
}
