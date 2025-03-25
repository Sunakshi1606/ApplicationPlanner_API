namespace ActivityPlannerAPI.Models
{
    public class GeocodingResponse
    {
        public required List<GeocodingResult> Results { get; set; }
    }

    public class GeocodingResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
    }
}
