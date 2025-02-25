using CarParkingSystem.Domain.Dtos.Location;

namespace CarParkingSystem.Domain.Helper
{
    public static class GetLocation
    {
        public static async Task<bool> IsLocationWithinRadius(string googleMapUrl,Location usrLocation,int radiusInKm = 3)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a request and follow redirects
                    HttpResponseMessage response = await client.GetAsync(googleMapUrl);
                    string fullUrl = response.RequestMessage.RequestUri.ToString();

                    if(response.RequestMessage.RequestUri is null) return false;

                    // Check if the URL contains latitude and longitude
                    if (fullUrl.Contains("/@"))
                    {
                        string[] parts = fullUrl.Split("@");
                        string[] coordinates = parts[1].Split(',');

                        string latitude = coordinates[0];
                        string longitude = coordinates[1];

                        var dealerloc = new Location()
                        {
                            Latitude = double.Parse(latitude),
                            Longitude = double.Parse(longitude)
                        };

                        var result = IsWithinRadius(usrLocation, dealerloc, radiusInKm);
                        return result;

                    }
                    else
                    {
                        Console.WriteLine("Full URL does not contain latitude and longitude.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool IsWithinRadius(Location userLocation, Location targetLocation, double radiusInKm)
        {
            double ToRadians(double degrees) => degrees * (Math.PI / 180);

            const double EarthRadiusKm = 6371.0; // Mean radius of Earth in kilometers

            // Convert latitude and longitude from degrees to radians
            double lat1 = ToRadians(userLocation.Latitude);
            double lon1 = ToRadians(userLocation.Longitude);
            double lat2 = ToRadians(targetLocation.Latitude);
            double lon2 = ToRadians(targetLocation.Longitude);

            // Compute differences
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            // Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c; // Distance in kilometers

            return distance <= radiusInKm;
        }
    }
}
