﻿using System;
using System.Collections.Generic;
using System.Text;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private bool tookPlace = false;
        private ICollection<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get => raceName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }
                raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get => numberOfLaps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
                numberOfLaps = value;
            }
        }

        public bool TookPlace
        {
            get => tookPlace;
            set
            {
                tookPlace = value;
            }
        }

        public ICollection<IPilot> Pilots => pilots;

        public void AddPilot(IPilot pilot)
        {
            pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            string raceTrueFalse = TookPlace ? "Yes" : "No";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {this.RaceName} race has:");
            sb.AppendLine($"Participants: {Pilots.Count}");
            sb.AppendLine($"Number of laps: {NumberOfLaps}");
            sb.AppendLine($"Took place: {raceTrueFalse}");

            return sb.ToString().TrimEnd();
        }
    }
}
