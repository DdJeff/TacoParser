using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
        
            logger.LogInfo("Log initialized");

            //get data
            var lines = File.ReadAllLines(csvPath);

            //validate data
            if (lines.Length == 0)
            {
                logger.LogError("Something when wrong.There is no data.");
            }if(lines.Length == 1)
            {
                logger.LogWarning("Not all data could be parsed.");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations
            var locations = lines.Select(parser.Parse).ToArray();
    

            // Create a `double` variable to store the distance

            ITrackable locale1 = null;
            ITrackable locale2 = null;
            double distance = 0.0;


          
            // Grabing each location
            for (int i = 0; i < locations.Length; i++)
            {
               
                // Create a new corA Coordinate with your locA's lat and long

                var locA = locations[i] ;
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB= locations[j];

                    // Create a new Coordinate with your locB's lat and long
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    // Now, compare the two using `.GetDistanceTo()`, which returns a double 
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        locale1 = locA;
                        locale2 = locB;
                    }  
                        
                }
             
            }//outter loop

            // the two Taco Bells farthest away from each other.

            logger.LogInfo($"The two furthest taco Bells are {locale1.Name} and {locale2.Name}");
           
        }
    }
}
