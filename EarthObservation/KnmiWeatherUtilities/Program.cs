using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KnmiWeatherUtilities.Data;

namespace KnmiWeatherUtilities
{
    class Program
    {
        static void Main(string[] args)
        {
            StringDataRequest request = new StringDataRequest();

            KnmiParameters parameters = new KnmiParameters()
            {
                Stations = new[] {"260"}, InSeason = false, StartDate = new DateTime(2015, 01, 01),
                EndDate = new DateTime(2015, 01, 31), Variables = new[] {"TG", "PG"}
            };
            var result = request.ExecuteRequest(new KnmiDataRequestParser(parameters), new FormUrlEncodedContent(parameters.ToDictionary()));
            var data = result.GetDataBetween(KnmiVariables.TG, new DateTime(2015, 1, 5), new DateTime(2015, 1, 10));

        }

    }
}
