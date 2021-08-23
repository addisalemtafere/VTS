using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Locations.Queries.GetVehicleCurrentPosition
{
    public class VehiclePositionDto
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public int VehicleId { get; set; }
        public string Locality { get; set; }
    }
}