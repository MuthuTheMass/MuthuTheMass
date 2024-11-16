namespace DatabaseMigrator.SqlHelper
{
    public static class SqlHelper
    {
        public static string clause(string queryString,string insertQuery)
        {
            if(queryString is not null)
            {
                if (queryString.ToLower().Contains("where"))
                {
                    queryString += " AND " + insertQuery;
                }
                else
                {
                    queryString += " WHERE " + insertQuery;
                }
            }

            return queryString;
        }

        public static string jsonValueTiming(string cycle,string timing)
        {
            List<string> days = new() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            var resultString = string.Empty;

            days.ForEach(d => {
                if (cycle.Contains("Start")) {
                    resultString += $"JSON_VALUE([DealerTiming], '$.{d}.Start') = '{timing}'";
                }
                else if (cycle.Contains("Stop")){
                    resultString += $"JSON_VALUE([DealerTiming], '$.{d}.Stop') = '{timing}'";
                }
                if (!d.Contains("Sunday"))
                {
                    resultString += " AND ";
                }
            });

            return resultString;
        }
    }
}
