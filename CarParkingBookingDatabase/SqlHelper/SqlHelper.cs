using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingBookingDatabase.SqlHelper
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
    }
}
