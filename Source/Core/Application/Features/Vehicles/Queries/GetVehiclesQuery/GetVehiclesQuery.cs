using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetVehiclesQuery
{
    public class GetVehiclesQuery : IRequest<GetVehiclesQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}