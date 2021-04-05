using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CarFeature:IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Mileage { get; set; }
        public string Transmission { get; set; }
        public string Seats { get; set; }
        public string Luggage { get; set; }
        public string Fuel { get; set; }
    }
}
