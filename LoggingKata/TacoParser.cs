namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log that and return null
                logger.LogError("Error");
                // Do not fail if one record parsing fails, return null
                return null; // TODO Implement
            }

            // grab the latitude from your array at index 0
            var latitude = double.Parse(cells[0]);

            // grab the longitude from your array at index 1
            var longitude = double.Parse(cells[1]);

            // grab the name from your array at index 2
            var name = cells[2];



            TacoBell store = new TacoBell();

            // With the name and point set correctly
            //seeting name
             store.Name = name;

            //setting point
            var point = new Point();
            point.Latitude = latitude;
            point.Longitude = longitude;

            //updating points
            store.Location = point;


            return store;
        }
    }
}