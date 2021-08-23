﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Locations.Queries.GetVehicleCurrentPosition
{
    public class VehicleCurrentLocationDto
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public int VehicleId { get; set; }
        public string Locality { get; set; }
    }
}