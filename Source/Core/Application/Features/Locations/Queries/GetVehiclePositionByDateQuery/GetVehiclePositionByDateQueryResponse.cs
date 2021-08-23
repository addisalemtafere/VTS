﻿using System.Collections.Generic;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Application.Responses;

namespace Application.Features.Locations.Queries.GetVehiclePositionByDateQuery
{
    public class GetVehiclePositionByDateQueryResponse : BaseResponse
    {
        public GetVehiclePositionByDateQueryResponse() : base()
        {
        }

        public List<VehiclePositionDto> VehiclePosition { get; set; }
    }
}