using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Models
{
    /// <summary>
    /// Venicle Details
    /// </summary>
    public class APN_Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Edition { get; set; }
        public string ModelYear { get; set; }
        public string Mileage { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public bool IsNegotiate { get; set; }
        public string ContactNumber { get; set; }
        public bool HideNumber { get; set; }
    }

    /// <summary>
    /// Vehicla body condition
    /// </summary>
    public enum Condition
    {
        New,
        Used,
        Reconditioned
    }

    /// <summary>
    /// Vehicla Body type
    /// </summary>
    public enum BodyType
    {
        Saloon,
        Hatchback,
        Stationwagon,
        Convertible,
        Sports,
        SUV,
        MPV
    }

    /// <summary>
    /// vehicle transmission
    /// </summary>
    public enum Transmission
    {
        Manual,
        Automatic,
        Tiptronic,
        Other
    }

    /// <summary>
    /// FuelType
    /// </summary>
    public enum FuelType
    {
        Diesel,
        Petrol,
        Hybrid,
        Electric,
        CNG,
        Other
    }
}
